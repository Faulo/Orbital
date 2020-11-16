using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    Image barTeamYellow => GameManager.instance.GetTeam(TeamColor.Yellow).scoreBar;
    Image barTeamGreen => GameManager.instance.GetTeam(TeamColor.Green).scoreBar;

    // Parameters for balancing
    [SerializeField, Range(1, 100000)]
    int pointsToWin = 100;

    // Variables
    float pointsTeamYellow; // Team Index: 1
    float pointsTeamGreen; // Team Index: 2

    float fillPerPoint;

    [SerializeField, Range(0, 1)]
    float fillSpeed = 1f;

    public void Init(GameObject mapParent) {
        pointsTeamYellow = 0;
        pointsTeamGreen = 0;

        fillPerPoint = 1.0f / pointsToWin;
        barTeamYellow.fillAmount = 0.0f;
        barTeamGreen.fillAmount = 0.0f;

        InvokeRepeating("UpdateScore", 1.0f, 1.0f);
    }
    void Start() {

    }
    void Update() {
        barTeamYellow.fillAmount = Mathf.Lerp(barTeamYellow.fillAmount, pointsTeamYellow * fillPerPoint, fillSpeed);
        barTeamGreen.fillAmount = Mathf.Lerp(barTeamGreen.fillAmount, pointsTeamGreen * fillPerPoint, fillSpeed);
    }


    void UpdateScore() {
        var capturables = FindObjectsOfType<GameObject>()
            .SelectMany(obj => obj.GetComponents<ICapturable>());
        foreach (var capturable in capturables) {
            switch (capturable.belongsTo) {
                case TeamColor.Yellow:
                    pointsTeamYellow += capturable.worth;
                    break;
                case TeamColor.Green:
                    pointsTeamGreen += capturable.worth;
                    break;
            }
        }
        if (pointsTeamYellow >= pointsToWin) {
            GetComponent<GameManager>().GameOver(TeamColor.Yellow);
        }

        if (pointsTeamGreen >= pointsToWin) {
            GetComponent<GameManager>().GameOver(TeamColor.Green);
        }
    }
}