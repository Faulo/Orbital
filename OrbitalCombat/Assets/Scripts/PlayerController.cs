using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField, Range(1, 4)]
    private int playerID = 1;

    [SerializeField, Range(0, 10)]
    private float maxThrust = 1;

    [SerializeField, Range(0, 10)]
    private float rotationSpeed = 1;

    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Boost();
    }

    private void Boost() {
		var input = new Vector2(Input.GetAxis("Horizontal" + playerID), Input.GetAxis("Vertical" + playerID));

		if (input.magnitude > 0)
		{
			float inputAngle = Vector2.SignedAngle(Vector2.up, input);
			Quaternion rot = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(inputAngle, Vector3.forward), rotationSpeed);

			transform.rotation = rot;
			rb.AddForce(transform.up * maxThrust * input.magnitude);
		}
	}

	private void ShootRocket()
	{

	}
}