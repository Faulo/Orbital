using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable {
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

    private int inputId;

    public Color color { get; private set; }

    [SerializeField, Range(0, 100)]
    private float maxThrust = 1;

    [SerializeField, Range(0, 100)]
    private float rotationSpeed = 1;

    [SerializeField]
    private GameObject missilePrefab = default;
    [SerializeField, Range(0, 5)]
    private float missileInterval = 1;
    [SerializeField, Range(0, 10)]
    private float missileLaunchSpeed = 1;

    private new Rigidbody2D rigidbody;
    private Coroutine shootingRocket;
    private Coroutine dying;
    public bool isAlive { get; private set; } = true;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Boost();
        if (Input.GetButton("Fire" + inputId) && shootingRocket == null) {
            shootingRocket = StartCoroutine(ShootRocketRoutine());
        }
    }

    private void Boost() {
        var input = new Vector2(Input.GetAxis("Horizontal" + inputId), Input.GetAxis("Vertical" + inputId));

        if (input.magnitude > 0) {
            float inputAngle = Vector2.SignedAngle(Vector2.up, input);
            Quaternion rot = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(inputAngle, Vector3.forward), rotationSpeed);

            transform.rotation = rot;

            var dragBonus = 1 + rigidbody.drag;
            rigidbody.AddForce(transform.up * maxThrust * input.magnitude * dragBonus * rigidbody.mass);
        }
    }

    private IEnumerator ShootRocketRoutine() {
        var missile = Instantiate(missilePrefab, transform.position + transform.up.normalized, transform.rotation).GetComponent<Missile>();
        missile.teamColor = teamColor;
        missile.sprite = team.missile;
        missile.AddVelocity(rigidbody.velocity);
        missile.AddVelocity(missileLaunchSpeed * transform.up);
        yield return new WaitForSeconds(missileInterval);
        shootingRocket = null;
    }

    public void TakeDamage(float value) {
        if (dying == null) {
            dying = StartCoroutine(DieRoutine());
        }
    }

    private IEnumerator DieRoutine() {
        isAlive = false;
        transform.position = Vector3.one * 1000;
        yield return new WaitForSeconds(1);
        Respawn();
    }
    private void Respawn() {
        rigidbody.velocity = Vector3.zero;
        transform.position = config.spawn.position;
        transform.rotation = config.spawn.rotation;
        isAlive = true;
        dying = null;
    }
}