using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class OneShotParticles : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Invoke("Die", GetComponent<ParticleSystem>().main.duration);
    }

    // Update is called once per frame
    void Die() {
        Destroy(gameObject);
    }
}
