using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Image barTeamYellow;
    public Image barTeamGreen;

    // Parameters for balancing
    private readonly int[] pointsPerPlanet = new int[3]
    {
        3,
        2,
        1
    };
    private readonly int pointsToWin = 100;

    // Variables
    private PlanetManager[] planets;
    private int pointsTeamYellow; // Team Index: 1
    private int pointsTeamGreen; // Team Index: 2

    private float fillPerPoint;

    public void Init(GameObject mapParent)
    {
        planets = mapParent.GetComponentsInChildren<PlanetManager>();

        pointsTeamYellow = 0;
        pointsTeamGreen = 0;

        fillPerPoint = 1.0f / pointsToWin;
        barTeamYellow.fillAmount = 0.0f;
        barTeamGreen.fillAmount = 0.0f;

        InvokeRepeating("UpdateScore", 1.0f, 1.0f);
    }

    private void Update ()
    {
        if (barTeamYellow.fillAmount < pointsTeamYellow * fillPerPoint)
            barTeamYellow.fillAmount += 0.0005f;

        if (barTeamGreen.fillAmount < pointsTeamGreen * fillPerPoint)
            barTeamGreen.fillAmount += 0.0005f;
    }
    
    private void UpdateScore()
    {
        foreach (PlanetManager planet in planets)
        {
            if (planet.GetTeamIndex() == 1)
            {
                pointsTeamYellow += pointsPerPlanet[planet.GetRemainingMass() - 1];
            }
            else if (planet.GetTeamIndex() == 2)
            {
                pointsTeamGreen += pointsPerPlanet[planet.GetRemainingMass() - 1];
            }
        }

        if (pointsTeamYellow >= pointsToWin)
            GetComponent<GameManager>().GameOver(1);
        if (pointsTeamGreen >= pointsToWin)
            GetComponent<GameManager>().GameOver(2);
    }
}