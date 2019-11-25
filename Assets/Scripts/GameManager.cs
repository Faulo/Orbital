
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ScoreManager))]
public class GameManager : MonoBehaviour {
    public static GameManager instance;

    [Range(2, 4)]
    public int numberOfPlayers;

    public GameObject playerPrefab;
    public PlayerConfig[] players;

    public TeamConfig GetTeam(TeamColor color) {
        return teams
            .Where(team => team.color == color)
            .First();
    }

    public TeamConfig[] teams;

    public GameObject[] mapPresets;

    private GameObject mapParent;
    private Planet[] planets;
    

    private void Awake() {
        if (!instance)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    private void Start() {
        mapParent = Instantiate(mapPresets[Random.Range(0, mapPresets.Length)], Vector3.zero, Quaternion.identity);
        planets = mapParent.GetComponentsInChildren<Planet>();

        for (int i = 0; i < numberOfPlayers; i++) {
            var player = Instantiate(playerPrefab).GetComponent<PlayerController>();
            player.config = players[i];
        }

        /*
        foreach (var planet in planets) {
            var random = Random.Range(0f, 1f);
            if (random > 0.7) {
                planet.belongsTo = TeamColor.Yellow;
            } else if (random > 0.4) {
                planet.belongsTo = TeamColor.Green;
            } else {
                planet.belongsTo = TeamColor.Nobody;
            }
        }
        //*/

        GetComponent<ScoreManager>().Init(mapParent);
    }

    public void GameOver(TeamColor winningTeam) {
        Time.timeScale = 0.1f;
        GetTeam(winningTeam).winScreen.gameObject.SetActive(true);
        Invoke("NextLevel", 0.5f);
    }
    void NextLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
}
