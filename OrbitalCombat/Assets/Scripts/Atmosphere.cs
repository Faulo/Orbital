using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Atmosphere : MonoBehaviour {
    public float radius {
        get => transform.localScale.x * collider.radius;
    }

    [SerializeField]
    private AnimationCurve airResistanceOverDistance = default;

    public new ParticleSystem particleSystem => GetComponent<ParticleSystem>();
    public new CircleCollider2D collider => GetComponent<CircleCollider2D>();

    void Update() {
    }

    void OnTriggerStay(Collider other) {
        if (other.attachedRigidbody) {
            var todo = Vector3.zero;
            other.attachedRigidbody.AddForce(todo);
        }
    }
}
