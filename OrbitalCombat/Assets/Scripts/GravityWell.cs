using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour {
    [SerializeField]
    private AnimationCurve gravityOverDistance = default;

    void OnTriggerStay(Collider other) {
        if (other.attachedRigidbody) {
            var todo = Vector3.zero;
            other.attachedRigidbody.AddForce(todo);
        }
    }
}
