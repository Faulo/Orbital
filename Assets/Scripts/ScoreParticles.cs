using System.Collections;
using System.Collections.Generic;
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
    private new ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    private Image bar;

    [SerializeField]
    private AnimationCurve sizeOverWorth = default;

    void Awake() {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }

    private void Start() {
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
