using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour {
    public TeamColor teamColor;
    public Sprite sprite {
        set {
            GetComponentInChildren<SpriteRenderer>().sprite = value;
        }
    }
    private new Rigidbody2D rigidbody => GetComponent<Rigidbody2D>();
    public void AddVelocity(Vector2 velocity) {
        rigidbody.AddForce(velocity, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.rigidbody) {
            collision.rigidbody.AddForce(transform.up * rigidbody.mass, ForceMode2D.Impulse);
            var damageables = collision.rigidbody
                .GetComponentsInChildren<IDamageable>()
                .Where(damageable => damageable.teamColor != teamColor);
            foreach (var damageable in damageables) {
                damageable.TakeDamage(rigidbody.mass);
            }
            Destroy(gameObject);
        }
    }
}
