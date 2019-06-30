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
            transform.LookAt(team.scoreBar.transform);
        }
    }
    private new ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    private Image bar;

    void Awake() {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }

    void Update() {
        particleSystem.GetParticles(particles);
        //foreach (particleSystem.particl)
    }
}
