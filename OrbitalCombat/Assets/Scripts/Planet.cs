using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class Planet : MonoBehaviour, ICapturable {
    [SerializeField]
    private Sprite nobodyOrbit = default;
    [SerializeField]
    private Sprite yellowOrbit = default;
    [SerializeField]
    private Sprite greenOrbit = default;

    [SerializeField, Range(0, 20)]
    public float coreRadius = 0;
    [SerializeField, Range(0, 20)]
    public float atmosphereRadius = 0;
    [SerializeField, Range(0, 20)]
    public float gravityRadius = 0;
    [SerializeField, Range(0, 10000)]
    public float coreDensity = 0;

    public float coreMass {
        get => coreRadius * coreDensity;
    }
    public TeamColor belongsTo {
        get => belongsToCache;
        set {
            belongsToCache = value;
            switch (belongsToCache) {
                case TeamColor.Nobody:
                    atmosphere.renderer.sprite = nobodyOrbit;
                    break;
                case TeamColor.Yellow:
                    AudioManager.instance.Play("YellowPlanetCapture");
                    atmosphere.renderer.sprite = yellowOrbit;
                    break;
                case TeamColor.Green:
                    AudioManager.instance.Play("GreenPlanetCapture");
                    atmosphere.renderer.sprite = greenOrbit;
                    break;
            }
        }
    }
    private TeamColor belongsToCache;

    public float worth => coreRadius;

    Core core => GetComponentInChildren<Core>();
    Atmosphere atmosphere => GetComponentInChildren<Atmosphere>();
    Gravity gravity => GetComponentInChildren<Gravity>();

    void Start() {
    }

    void Update() {

#if UNITY_EDITOR
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
#endif
    }
}
