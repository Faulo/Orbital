using Slothsoft.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject[] asteroidPrefabs = default;
    [SerializeField, Range(0, 10)]
    private float asteroidInterval = 1;
    [SerializeField, Range(0, 10)]
    private float asteroidLaunchSpeed = 1;
    [SerializeField]
    private AnimationCurve asteroidSizeDistribution = default;

    private float asteroidTimer;

    private IEnumerable<BoxCollider2D> boxes;

    // Start is called before the first frame update
    void Start() {
        boxes = GetComponentsInParent<BoxCollider2D>();
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
        var asteroid = Instantiate(asteroidPrefabs.RandomElement(), RandomPosition(), Quaternion.identity).GetComponent<Asteroid>();
        asteroid.size = RandomSize();
        asteroid.LaunchTowards(Vector3.zero, asteroidLaunchSpeed);
    }
    private float RandomSize() {
        return asteroidSizeDistribution.Evaluate(Random.value);
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
