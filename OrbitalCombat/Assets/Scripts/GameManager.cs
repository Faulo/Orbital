using UnityEngine;

 [RequireComponent(typeof(ScoreManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Range(2,4)]
    public int numberOfPlayers;
    public GameObject[] mapPresets;

    private GameObject mapParent;
    private GameObject[] planets;

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
        planets = new GameObject[mapParent.transform.childCount];
        for (int j = 0; j < mapParent.transform.childCount; j++)
        {
            planets[j] = mapParent.transform.GetChild(j).gameObject;
            planets[j].GetComponent<PlanetManager>().Init();
        }

        GetComponent<ScoreManager>().Init(mapParent);

        planets[0].GetComponent<PlanetManager>().SetToTeam(1);
        planets[2].GetComponent<PlanetManager>().SetToTeam(2);
        planets[3].GetComponent<PlanetManager>().SetToTeam(2);
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
