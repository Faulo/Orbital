using UnityEngine;

public class PlayerController : MonoBehaviour {
    public PlayerConfig config {
        set {
            inputId = value.inputId;
            transform.position = value.spawn.position;
            GetComponentInChildren<SpriteRenderer>().sprite = value.spaceship;
            GetComponentInChildren<SpriteRenderer>().color = value.color;
            team = GameManager.instance.GetTeam(value.team);
        }
    }
    public TeamConfig team;

    private int inputId;

    [SerializeField, Range(0, 100)]
    private float maxThrust = 1;

    [SerializeField, Range(0, 100)]
    private float rotationSpeed = 1;

    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Boost();
    }

    private void Boost() {
        var input = new Vector2(Input.GetAxis("Horizontal" + inputId), Input.GetAxis("Vertical" + inputId));

        if (input.magnitude > 0) {
            float inputAngle = Vector2.SignedAngle(Vector2.up, input);
            Quaternion rot = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(inputAngle, Vector3.forward), rotationSpeed);

            transform.rotation = rot;

            var dragBonus = 1 + rb.drag;
            rb.AddForce(transform.up * maxThrust * input.magnitude * dragBonus);
        }
    }

    private void ShootRocket() {

    }
}