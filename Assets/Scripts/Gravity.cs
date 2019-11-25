using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gravity : MonoBehaviour {
    public new CircleCollider2D collider => GetComponent<CircleCollider2D>();
    public Planet planet => GetComponentInParent<Planet>();

    [SerializeField]
    private bool gravModActive = true;

    [SerializeField]
    private AnimationCurve gravModOverVelocity = default;

    [SerializeField]
    private float gravModMinVelocity = 4;

    [SerializeField]
    private float gravModMaxVelocity = 10;

    [SerializeField]
    private AnimationCurve gravityOverDistance = default;

    void OnTriggerStay2D(Collider2D other) {
		//Debug.Log(other.name);
        if (other.attachedRigidbody) {
            Vector2 directionNorm = (transform.position - other.transform.position).normalized;
            float dist = Vector2.Distance(transform.position, other.transform.position) - planet.coreRadius;
			float t = dist / (planet.gravityRadius - planet.coreRadius);

			float velocity = other.attachedRigidbody.velocity.magnitude;
			float tGravMod = (velocity - gravModMinVelocity) / (gravModMaxVelocity - gravModMinVelocity);
			float gravMod = gravModActive
                ? gravModOverVelocity.Evaluate(t)
                : 1;

			float gravitationForce = gravityOverDistance.Evaluate(t) * other.attachedRigidbody.mass * planet.coreMass * gravMod;

            other.attachedRigidbody.AddForce(directionNorm * gravitationForce);

			
        }
    }
}
