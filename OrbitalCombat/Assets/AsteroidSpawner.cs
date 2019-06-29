using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject asteroidPrefab = default;
    [SerializeField, Range(0, 10)]
    private float asteroidInterval = 1;
    [SerializeField, Range(0, 10)]
    private float asteroidLaunchSpeed = 1;

    private float asteroidTimer;

    private IEnumerable<BoxCollider2D> boxes => GetComponents<BoxCollider2D>();

    // Start is called before the first frame update
    void Start() {
        if (asteroidInterval < Mathf.Epsilon) {
            asteroidInterval = Mathf.Epsilon;
        }
    }

    // Update is called once per frame
    void Update() {
        asteroidTimer += Time.deltaTime;
        if (asteroidTimer > asteroidInterval) {
            asteroidTimer -= asteroidInterval;
            Spawn();
        }
    }

    private void Spawn() {
        var asteroid = Instantiate(asteroidPrefab, RandomPosition(), Quaternion.identity).GetComponent<Asteroid>();
        asteroid.LaunchTowards(Vector3.zero, asteroidLaunchSpeed);
    }
    private Vector3 RandomPosition() {
        var rect = boxes.RandomElement().bounds;
        return new Vector3(
            Random.Range(rect.min.x, rect.max.x),
            Random.Range(rect.min.y, rect.max.y),
            Random.Range(rect.min.z, rect.max.z)
        );
    }
}
