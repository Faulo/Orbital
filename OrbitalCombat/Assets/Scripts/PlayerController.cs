using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float playerID;
    public float maxThrust;

    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Boost();
    }

    private void Boost() {
        Vector2 input = Vector2.zero;
        switch (playerID) {
            case 1: {
                    input = new Vector2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1"));
                    break;
                }
            case 2: {
                    input = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
                    break;
                }
            case 3: {
                    input = new Vector2(Input.GetAxis("Horizontal3"), Input.GetAxis("Vertical3"));
                    break;
                }
            case 4: {
                    input = new Vector2(Input.GetAxis("Horizontal4"), Input.GetAxis("Vertical4"));
                    break;
                }
        }

        rb.AddForce(input * maxThrust);
    }
}