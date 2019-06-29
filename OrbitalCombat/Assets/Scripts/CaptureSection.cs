using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureSection : MonoBehaviour, ICapturable {
    public Planet planet { get; set; }
    public Color color {
        set {
            line.startColor = new Color(value.r, value.g, value.b, line.startColor.a);
            line.endColor = new Color(value.r, value.g, value.b, line.endColor.a);
        }
    }
    public Vector3 position => line.GetPosition(0);

    public TeamColor belongsTo {
        get => belongsToCache;
        set {
            belongsToCache = value;
            color = GameManager.instance.GetTeam(value).capturingColor;
        }
    }
    private TeamColor belongsToCache;
    public float worth => planet.worth;

    private LineRenderer line;

    // Start is called before the first frame update
    void Awake() {
        line = GetComponent<LineRenderer>();
    }

    public void SetPositions(Vector3 start, Vector3 end) {
        line.SetPosition(1, start);
        line.SetPosition(0, end);
    }
    public void SetWidth(float width) {
        line.widthMultiplier = width;
    }
}
