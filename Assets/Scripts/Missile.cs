using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour {
    float size => rigidbody.mass;

    [SerializeField]
    GameObject explosionPrefab = default;

    public TeamColor teamColor;
    public Sprite sprite {
        set {
            GetComponentInChildren<SpriteRenderer>().sprite = value;
        }
    }
    new Rigidbody2D rigidbody => GetComponent<Rigidbody2D>();
    Vector3 velocity = default;
    public void AddVelocity(Vector2 velocity) {
        this.velocity += new Vector3(velocity.x, velocity.y, 0);
        //rigidbody.AddForce(velocity, ForceMode2D.Impulse);
    }
    public void AddVelocity(Vector3 velocity) {
        this.velocity += velocity;
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.rigidbody) {
            collision.rigidbody.AddForce(transform.up * rigidbody.mass, ForceMode2D.Impulse);
            var damageables = collision.rigidbody
                .GetComponentsInChildren<IDamageable>()
                .Where(damageable => damageable.teamColor != teamColor);
            foreach (var damageable in damageables) {
                damageable.TakeDamage(rigidbody.mass);
            }
            Explode();
        } else {
            var planet = collision.gameObject.GetComponentInParent<Planet>();
            if (planet) {
                planet.GrowBy(-size);
                Explode();
            }
        }
    }
    void FixedUpdate() {
        transform.Translate(velocity * Time.fixedDeltaTime, Space.World);
    }
    public void Explode() {
        AudioManager.instance.PlayOneShot("Missle Hit");
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.transform.localScale = Vector3.one * size;
        Destroy(gameObject);
    }
}
