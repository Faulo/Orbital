using UnityEngine;

[ExecuteInEditMode]
public class Planet : MonoBehaviour, ICapturable {
    [SerializeField]
    Sprite nobodyOrbit = default;
    [SerializeField]
    Sprite yellowOrbit = default;
    [SerializeField]
    Sprite greenOrbit = default;

    [SerializeField, Range(0, 20)]
    public float coreRadius = 0;
    [SerializeField, Range(0, 20)]
    public float atmosphereRadius = 0;
    [SerializeField, Range(0, 20)]
    public float gravityRadius = 0;
    [SerializeField, Range(0, 10000)]
    public float coreDensity = 0;
    [SerializeField]
    AnimationCurve worthOverMass = default;

    public float coreMass {
        get => coreRadius * coreDensity;
    }
    public TeamColor belongsTo {
        get => belongsToCache;
        set {
            if (belongsToCache != value) {
                belongsToCache = value;
                particles.team = value;
                switch (belongsToCache) {
                    case TeamColor.Nobody:
                        atmosphere.renderer.sprite = nobodyOrbit;
                        break;
                    case TeamColor.Yellow:
                        AudioManager.instance.PlayOneShot("PlanetCaptureYellow");
                        atmosphere.renderer.sprite = yellowOrbit;
                        break;
                    case TeamColor.Green:
                        AudioManager.instance.PlayOneShot("PlanetCaptureGreen");
                        atmosphere.renderer.sprite = greenOrbit;
                        break;
                }
            }
        }
    }

    public void GrowBy(float size) {
        coreRadius += size / 10;
        atmosphereRadius += size / 10;
        gravityRadius += size / 10;
    }

    TeamColor belongsToCache;

    public float worth => worthOverMass.Evaluate(coreMass);

    Core core;
    Atmosphere atmosphere;
    Gravity gravity;
    ScoreParticles particles;

    void Start() {
        core = GetComponentInChildren<Core>();
        atmosphere = GetComponentInChildren<Atmosphere>();
        gravity = GetComponentInChildren<Gravity>();
        particles = GetComponentInChildren<ScoreParticles>();
    }

    void Update() {
        if (atmosphereRadius < coreRadius) {
            atmosphereRadius = coreRadius;
        }
        if (gravityRadius < atmosphereRadius) {
            gravityRadius = atmosphereRadius;
        }

        core.transform.localScale = Vector3.one * coreRadius / core.collider.radius;

        atmosphere.transform.localScale = Vector3.one * atmosphereRadius / atmosphere.collider.radius;

        //var shape = atmosphere.particleSystem.shape;
        //shape.radiusThickness = 1 - coreRadius / atmosphereRadius;

        gravity.transform.localScale = Vector3.one * gravityRadius / gravity.collider.radius;
    }
}
