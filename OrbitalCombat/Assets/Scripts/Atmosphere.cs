using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Atmosphere : MonoBehaviour {

    [SerializeField]
    private AnimationCurve dragOverDistance = default;
	[SerializeField]
	private float maxDrag;

	public new ParticleSystem particleSystem => GetComponent<ParticleSystem>();
    public new CircleCollider2D collider => GetComponent<CircleCollider2D>();
    public new SpriteRenderer renderer => GetComponent<SpriteRenderer>();

    private Planet planet;

	void Start()
	{
		planet = transform.parent.GetComponent<Planet>();
	}

	void Update() {
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		//Debug.Log(collision.name);
		if (collision.attachedRigidbody)
		{
			var todo = Vector3.zero;
			float dist = Vector2.Distance(transform.position, collision.transform.position) - planet.coreRadius;
			float t = dist / (planet.atmosphereRadius - planet.coreRadius);
			collision.attachedRigidbody.drag = dragOverDistance.Evaluate(t) * maxDrag;

		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.attachedRigidbody)
		{
			collision.attachedRigidbody.drag = 0;
		}
	}
}
