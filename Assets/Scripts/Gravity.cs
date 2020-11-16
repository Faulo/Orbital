using UnityEngine;


public class Gravity : MonoBehaviour {
    public new CircleCollider2D collider => GetComponent<CircleCollider2D>();
    public Planet planet => GetComponentInParent<Planet>();

    [SerializeField]
    bool gravModActive = true;

    [SerializeField]
    AnimationCurve gravModOverVelocity = default;

    [SerializeField]
    float gravModMinVelocity = 4;

    [SerializeField]
    float gravModMaxVelocity = 10;

    [SerializeField]
    AnimationCurve gravityOverDistance = default;

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
