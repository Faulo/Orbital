using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gravity : MonoBehaviour {
    public float radius {
        get => transform.localScale.x * collider.radius;
    }
    public new CircleCollider2D collider => GetComponent<CircleCollider2D>();

    [SerializeField]
    private AnimationCurve gravityOverDistance = default;

    void OnTriggerStay(Collider other) {
        if (other.attachedRigidbody) {
            var todo = Vector3.zero;
            other.attachedRigidbody.AddForce(todo);
        }
    }
}
