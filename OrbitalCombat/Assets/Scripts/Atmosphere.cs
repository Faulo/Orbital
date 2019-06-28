using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atmosphere : MonoBehaviour {
    [SerializeField]
    private AnimationCurve airResistanceOverDistance = default;

    void OnTriggerStay(Collider other) {
        if (other.attachedRigidbody) {
            var todo = Vector3.zero;
            other.attachedRigidbody.AddForce(todo);
        }
    }
}
