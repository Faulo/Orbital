using UnityEngine;


public class Core : MonoBehaviour {
    public float radius {
        get => transform.localScale.x * collider.radius;
    }
    public new CircleCollider2D collider => GetComponent<CircleCollider2D>();
    public new SpriteRenderer renderer => GetComponent<SpriteRenderer>();

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
