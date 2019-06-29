using System.Collections.Generic;
using UnityEngine;

 [RequireComponent(typeof(ScoreManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Range(2,4)]
    public int numberOfPlayers;
    public GameObject[] mapPresets;

    private GameObject mapParent;
    private Planet[] planets;

    private void Awake ()
    {
        if (!instance)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    private void Start()
    {
        mapParent = Instantiate(mapPresets[Random.Range(0, mapPresets.Length)], Vector3.zero, Quaternion.identity);
        planets = mapParent.GetComponentsInChildren<Planet>();
        foreach (var planet in planets) {
            if (Random.Range(0f, 1f) > 0.5f) {
                planet.belongsTo = TeamColor.Yellow;
            } else {
                planet.belongsTo = TeamColor.Green;
            }
        }

        GetComponent<ScoreManager>().Init(mapParent);
    }

    public void GameOver (int winningTeam)
    {
        Time.timeScale = 0.0f;

        if (winningTeam == 1)
        {
            Debug.Log("Team Yellow wins!");
        }
        else if (winningTeam == 2)
        {
            Debug.Log("Team Green wins!");
        }
    }
}
