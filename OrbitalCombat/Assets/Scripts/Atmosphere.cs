using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Atmosphere : MonoBehaviour {

    [SerializeField]
    private AnimationCurve dragOverDistance = default;
    [SerializeField, Range(0, 2)]
    private float maxDrag = 0;

    public new ParticleSystem particleSystem => GetComponent<ParticleSystem>();
    public new CircleCollider2D collider => GetComponent<CircleCollider2D>();
    public new SpriteRenderer renderer => GetComponent<SpriteRenderer>();

    private Planet planet;

    [SerializeField]
    private GameObject linePrefab = default;
    public CaptureSection[] lines;

    void Start() {
        planet = transform.parent.GetComponent<Planet>();
        lines = new CaptureSection[12];
        for (int i = 0; i < lines.Length; i++) {
            lines[i] = Instantiate(linePrefab).GetComponent<CaptureSection>();
            lines[i].planet = planet;
            lines[i].color = Color.white;
        }
        UpdateRadius(planet.atmosphereRadius);
    }

    void Update() {
        var teams = lines
            .Select(line => line.belongsTo)
            .Distinct();
        if (teams.Count() == 1) {
            planet.belongsTo = teams.First();
        }
        UpdateRadius(planet.atmosphereRadius);
    }
    void UpdateRadius(float radius) {
        for (int i = 0; i < lines.Length; i++) {
            var line = lines[i];
            var offset = new Vector3(Mathf.Sin(2 * Mathf.PI * i / lines.Length), Mathf.Cos(2 * Mathf.PI * i / lines.Length)) * radius;
            line.SetPositions(transform.position, transform.position + offset);
            line.SetWidth(radius);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        //Debug.Log(collision.name);
        if (collision.attachedRigidbody) {
            var todo = Vector3.zero;
            float dist = Vector2.Distance(transform.position, collision.transform.position) - planet.coreRadius;
            float t = dist / (planet.atmosphereRadius - planet.coreRadius);
            collision.attachedRigidbody.drag = dragOverDistance.Evaluate(t) * maxDrag;

            var player = collision.attachedRigidbody.GetComponent<PlayerController>();
            if (player) {
                var line = lines
                    .OrderBy(l => Vector3.Distance(player.transform.position, l.position))
                    .First();
                line.belongsTo = player.team.color;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.attachedRigidbody) {
            collision.attachedRigidbody.drag = 0;
        }
    }
}
