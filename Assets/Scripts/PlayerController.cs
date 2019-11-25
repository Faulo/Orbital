using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public PlayerConfig config {
        get => configCache;
        set {
            configCache = value;
            inputId = value.inputId;
            transform.position = value.spawn.position;
            GetComponentInChildren<SpriteRenderer>().sprite = value.spaceship;
            color = value.color;
            team = GameManager.instance.GetTeam(value.team);
        }
    }
    public PlayerConfig configCache;
    public TeamConfig team { get; private set; }
    public TeamColor teamColor => team.color;

    [SerializeField]
    private GameObject explosionPrefab = default;
    [SerializeField, Range(0, 10)]
    private float explosionSpeed = 1;

    private int inputId;

    public Color color { get; private set; }

    [SerializeField, Range(0, 100)]
    private float maxThrust = 1;

    [SerializeField, Range(0, 100)]
    private float rotationSpeed = 1;

    [SerializeField]
    private GameObject missilePrefab = default;
    [SerializeField, Range(0, 1)]
    private float missileTreshold = 0;
    [SerializeField, Range(0, 5)]
    private float missileInterval = 1;
    [SerializeField, Range(0, 100)]
    private float missileLaunchSpeed = 1;

    private new Rigidbody2D rigidbody;
    private Coroutine shootingRocket;

    [SerializeField, Range(0, 10)]
    private int respawnTime = 1;

    public bool isAlive { get; private set; } = true;

    private new ParticleSystem particleSystem;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void FixedUpdate() {
        if (!isAlive) {
            return;
        }
        Boost();
        Shoot();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.otherCollider.gameObject.layer == LayerMask.GetMask("Border")) {
            return;
        }
        var impulse = Vector3.Dot(collision.GetContact(0).normal, collision.relativeVelocity);
        if (impulse > explosionSpeed) {
            Explode();
        }
    }

    private void Boost() {
        var input = new Vector2(Input.GetAxis("Horizontal" + inputId), Input.GetAxis("Vertical" + inputId));

        var emission = particleSystem.emission;
        emission.rateOverTime = 500 * input.magnitude;

        if (input.magnitude > Mathf.Epsilon) {
            float inputAngle = Vector2.SignedAngle(Vector2.up, input);
            Quaternion rot = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(inputAngle, Vector3.forward), rotationSpeed);

            transform.rotation = rot;

            var dragBonus = 1 + rigidbody.drag;
            rigidbody.AddForce(transform.up * maxThrust * input.magnitude * dragBonus * rigidbody.mass);
        }
    }

    private void Shoot() {
        if (shootingRocket == null) {
            var input = new Vector3(Input.GetAxis("Horizontal"+ inputId+"b"), Input.GetAxis("Vertical"+ inputId+"b"), 0);
            
            if (input.magnitude > missileTreshold) {
                shootingRocket = StartCoroutine(ShootRocketRoutine(input));
            } else if (Input.GetButton("Fire" + inputId)) {
                shootingRocket = StartCoroutine(ShootRocketRoutine(transform.up));
            }
        }
    }
    private IEnumerator ShootRocketRoutine(Vector3 direction) {
        AudioManager.instance.PlayOneShot("ShootMissile" + team.color);
        var missile = Instantiate(missilePrefab, transform.position + direction.normalized, Quaternion.identity).GetComponent<Missile>();
        missile.teamColor = teamColor;
        missile.sprite = team.missile;
        float inputAngle = Vector2.SignedAngle(Vector2.up, direction);
        missile.transform.rotation = Quaternion.AngleAxis(inputAngle, Vector3.forward);
        missile.AddVelocity(missileLaunchSpeed * missile.transform.up);
        yield return new WaitForSeconds(missileInterval);
        shootingRocket = null;
    }

    public void Explode() {
        if (isAlive) {
            StartCoroutine(DieRoutine());
        }
    }

    private IEnumerator DieRoutine() {
        isAlive = false;
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        var main = explosion.GetComponent<ParticleSystem>().main;
        main.startColor = color;
        transform.position = Vector3.one * 1000;
        AudioManager.instance.PlayOneShot("ExplodingShip");
        yield return new WaitForSeconds(respawnTime);
        Respawn();
    }
    private void Respawn() {
        rigidbody.velocity = Vector3.zero;
        transform.position = config.spawn.position;
        transform.rotation = config.spawn.rotation;
        isAlive = true;
    }
}