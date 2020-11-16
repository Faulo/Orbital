using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlanetManager : MonoBehaviour {
    SpriteRenderer rendering;
    int belongsToTeam;
    int remainingMass;

    public void Init() {
        rendering = GetComponent<SpriteRenderer>();
        belongsToTeam = 0;
        remainingMass = 3;
    }

    public int GetTeamIndex() {
        return belongsToTeam;
    }

    public void SetToTeam(int teamIndex) {
        belongsToTeam = teamIndex;
        switch (teamIndex) {
            case 0: {
                rendering.color = Color.white;
                break;
            }
            case 1: {
                rendering.color = new Color(1.0f, 0.8252001f, 0.126f);
                break;
            }
            case 2: {
                rendering.color = new Color(0.2812904f, 0.699f, 0.08771764f);
                break;
            }
        }
    }

    public int GetRemainingMass() {
        return remainingMass;
    }

    public void ReduceMass() {
        remainingMass--;
    }
}