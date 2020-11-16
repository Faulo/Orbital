using UnityEngine;

public class ShipPhysic : MonoBehaviour {

    public float maxThrust;
    public float rotationSpeed;
    public float gravitationConstant;
    public AnimationCurve gravityWell;
    public AnimationCurve airResistance;
    public float maxAirResistance;


    Rigidbody2D rb;
    GameObject[] planets;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        FindPlanets();
    }

    // Update is called once per frame
    void Update() {

        //Rotate();

    }

    void FixedUpdate() {
        Boost();
        //Debug.Log("Sum of Forces: " + SumOfForces());
        rb.AddForce(SumOfForces());
        //AutoRotate();
        Rotate();
    }


    public void Boost() {
        //float thrust = maxThrust * Input.GetAxis("RightTrigger");
        if (Input.GetKey(KeyCode.Space)) {
            //Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            //rb.AddForce(input * maxThrust);
        }
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.AddForce(input * maxThrust);
        /*if (Input.GetKey(KeyCode.LeftShift))
		{
			rb.AddForce(transform.right * maxThrust);
			Debug.Log("test");
		}
		if (Input.GetKey(KeyCode.LeftControl))
		{
			rb.AddForce(transform.right * -maxThrust);
		}*/


    }



    public void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Planet") {
            float dist = Vector2.Distance(transform.position, collision.gameObject.transform.position);
            rb.drag = maxAirResistance * airResistance.Evaluate(dist);
        }
    }

    public void Rotate() {
        float rotation = Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0, 0, -rotation * rotationSpeed));
    }

    public void AutoRotate() {
        GameObject nearestPlanet = null;
        float nearestDist = float.MaxValue;
        foreach (var planet in planets) {
            float dist = Vector2.Distance(transform.position, planet.transform.position);
            if (dist < nearestDist) {
                nearestDist = dist;
                nearestPlanet = planet;
            }
        }

        if (nearestPlanet != null) {
            //float angle = Vector2.Angle(Vector2.up, nearestPlanet.transform.position - transform.position);
            //Debug.Log("Angle: " + angle);
            //transform.rotation = Quaternion.Euler(0, 0, angle);

            //transform.LookAt(nearestPlanet.transform.position);

            var relativeUp = nearestPlanet.transform.TransformDirection(Vector3.forward);
            var relativePos = nearestPlanet.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(relativePos, relativeUp);
        }
    }

    public Vector2 SumOfForces() {
        Vector2 forces = new Vector2(0, 0);

        foreach (var planet in planets) {
            float dist = Vector2.Distance(transform.position, planet.transform.position);
            float pMass = planet.GetComponent<PlanetPhysic>().mass;
            Vector2 directionNorm = (planet.transform.position - transform.position).normalized;
            //float gravitationForce = gravitationConstant * ((pMass * rb.mass) / (Mathf.Pow(dist, 2.5f)));
            float gravitationForce = gravityWell.Evaluate(dist) * pMass;
            //Debug.Log("Graviation Force: " + gravitationForce);
            Debug.Log(dist);
            forces += directionNorm * gravitationForce;
        }

        return forces;
    }

    public void FindPlanets() {
        planets = GameObject.FindGameObjectsWithTag("Planet");
    }
}
