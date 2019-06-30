using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    private Image barTeamYellow => GameManager.instance.GetTeam(TeamColor.Yellow).scoreBar;
    private Image barTeamGreen => GameManager.instance.GetTeam(TeamColor.Green).scoreBar;

    // Parameters for balancing
    [SerializeField, Range(1, 100000)]
    private int pointsToWin = 100;

    // Variables
    private IEnumerable<ICapturable> capturables;
    private float pointsTeamYellow; // Team Index: 1
    private float pointsTeamGreen; // Team Index: 2

    private float fillPerPoint;

    [SerializeField, Range(0, 1)]
    private float fillSpeed = 1f;

    public void Init(GameObject mapParent) {
        capturables = mapParent.GetComponentsInChildren<ICapturable>();

        pointsTeamYellow = 0;
        pointsTeamGreen = 0;

        fillPerPoint = 1.0f / pointsToWin;
        barTeamYellow.fillAmount = 0.0f;
        barTeamGreen.fillAmount = 0.0f;

        InvokeRepeating("UpdateScore", 1.0f, 1.0f);
    }
    private void Start() {
    }
    private void Update() {
        barTeamYellow.fillAmount = Mathf.Lerp(barTeamYellow.fillAmount, pointsTeamYellow * fillPerPoint, fillSpeed);
        barTeamGreen.fillAmount = Mathf.Lerp(barTeamGreen.fillAmount, pointsTeamGreen * fillPerPoint, fillSpeed);
    }


    private void UpdateScore() {
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

        if (pointsTeamYellow >= pointsToWin)
            GetComponent<GameManager>().GameOver(1);
        if (pointsTeamGreen >= pointsToWin)
            GetComponent<GameManager>().GameOver(2);
    }
}