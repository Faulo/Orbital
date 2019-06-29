using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gravity : MonoBehaviour {
    public new CircleCollider2D collider => GetComponent<CircleCollider2D>();
    public Planet planet => GetComponentInParent<Planet>();

    [SerializeField]
    private AnimationCurve gravityOverDistance = default;

    void OnTriggerStay(Collider other) {
        if (other.attachedRigidbody) {
            Vector2 directionNorm = (planet.transform.position - transform.position).normalized;
            float dist = Vector2.Distance(transform.position, other.transform.position);
            float gravitationForce = gravityOverDistance.Evaluate(dist) * other.attachedRigidbody.mass * planet.coreMass;
            
            other.attachedRigidbody.AddForce(directionNorm * gravitationForce);
        }
    }
}
