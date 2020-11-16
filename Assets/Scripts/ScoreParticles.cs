using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ParticleSystem))]
public class ScoreParticles : MonoBehaviour {
    public TeamColor team {
        set {
            if (value == TeamColor.Nobody) {
                particleSystem.Stop();
                return;
            }
            if (!particleSystem.isPlaying) {
                particleSystem.Play();
            }
            var team = GameManager.instance.GetTeam(value);
            var main = particleSystem.main;
            main.startColor = team.capturingColor;
            bar = team.scoreBar;
        }
    }
    new ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;
    Image bar;

    [SerializeField]
    AnimationCurve sizeOverWorth = default;

    void Awake() {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }

    void Start() {
        var parent = GetComponentInParent<ICapturable>();
        var main = particleSystem.main;
        main.startSize = sizeOverWorth.Evaluate(parent.worth);
        team = parent.belongsTo;
    }

    void Update() {
        if (bar) {
            var y = bar.transform.position.y;
            var x = bar.transform.position.x * bar.fillAmount * 2;
            transform.LookAt(Vector3.right * x + Vector3.up * y);

            var count = particleSystem.GetParticles(particles);
            //foreach (particleSystem.particl)
            for (int i = 0; i < count; i++) {
                if (particles[i].position.y > y) {
                    particles[i].remainingLifetime = 0;
                }
            }
            particleSystem.SetParticles(particles, count);
        }
    }
}
