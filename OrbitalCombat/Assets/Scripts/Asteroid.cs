using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable {
    [SerializeField]
    private GameObject explosionPrefab = default;
    [SerializeField, Range(0, 1)]
    private float stopSpeed = 1;

    public float size { get; set; }
    private Coroutine mergingRoutine;

    private new Rigidbody2D rigidbody => GetComponent<Rigidbody2D>();

    public TeamColor teamColor => TeamColor.Nobody;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.localScale = Vector3.one * size;
        rigidbody.mass = size;
    }

    public void LaunchTowards(Vector3 target, float speed) {
        //transform.LookAt(target, Vector3.forward);
        rigidbody.AddForce((target - transform.position).normalized * speed, ForceMode2D.Impulse);
    }

    public void TakeDamage(float value) {
        Explode();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        /*
        var player = collision.gameObject.GetComponentInParent<PlayerController>();
        if (player) {
            player.Explode();
            Explode();
            return;
        }
        //*/
        var planet = collision.gameObject.GetComponentInParent<Planet>();
        if (planet) {
            mergingRoutine = StartCoroutine(MergeWithPlanetRoutine(planet));
            return;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        var planet = collision.gameObject.GetComponentInParent<Planet>();
        if (planet && mergingRoutine != null) {
            StopCoroutine(mergingRoutine);
            mergingRoutine = null;
        }
    }
    IEnumerator MergeWithPlanetRoutine(Planet planet) {
        while (rigidbody.velocity.magnitude > stopSpeed) {
            yield return null;
        }
        planet.GrowBy(size);
        Merge();
    }

    public void Explode() {
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.transform.localScale = Vector3.one * size;
        AudioManager.instance.PlayOneShot("ExplodingAsteroid");
        Destroy(gameObject);
    }
    public void Merge() {
        AudioManager.instance.PlayOneShot("ExplodingAsteroid");
        Destroy(gameObject);
    }
}
