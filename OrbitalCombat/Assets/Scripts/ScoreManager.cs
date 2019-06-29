using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public Image barTeamYellow;
    public Image barTeamGreen;

    // Parameters for balancing
    private readonly int pointsToWin = 100;

    // Variables
    private IEnumerable<ICapturable> capturables;
    private float pointsTeamYellow; // Team Index: 1
    private float pointsTeamGreen; // Team Index: 2

    private float fillPerPoint;

    public void Init(GameObject mapParent) {
        capturables = mapParent.GetComponentsInChildren<ICapturable>();

        pointsTeamYellow = 0;
        pointsTeamGreen = 0;

        fillPerPoint = 1.0f / pointsToWin;
        barTeamYellow.fillAmount = 0.0f;
        barTeamGreen.fillAmount = 0.0f;

        InvokeRepeating("UpdateScore", 1.0f, 1.0f);
    }

    private void Update() {
        if (barTeamYellow.fillAmount < pointsTeamYellow * fillPerPoint)
            barTeamYellow.fillAmount += 0.0005f;

        if (barTeamGreen.fillAmount < pointsTeamGreen * fillPerPoint)
            barTeamGreen.fillAmount += 0.0005f;
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