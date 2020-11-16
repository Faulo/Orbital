using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable {
    [SerializeField]
    GameObject explosionPrefab = default;
    [SerializeField, Range(0, 1)]
    float stopSpeed = 1;

    public float size { get; set; }
    Coroutine mergingRoutine;

    new Rigidbody2D rigidbody => GetComponent<Rigidbody2D>();

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
    void OnCollisionEnter2D(Collision2D collision) {
        var planet = collision.gameObject.GetComponentInParent<Planet>();
        if (planet) {
            mergingRoutine = StartCoroutine(MergeWithPlanetRoutine(planet));
            return;
        }
    }
    void OnCollisionExit2D(Collision2D collision) {
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
