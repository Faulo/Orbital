using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float playerID;
    public float maxThrust;
	public float rotaionSpeed;

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
			Quaternion rot = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(inputAngle, Vector3.forward), rotaionSpeed);

			transform.rotation = rot;
			rb.AddForce(transform.up * maxThrust * input.magnitude);
		}
	}

	private void ShootRocket()
	{

	}
}