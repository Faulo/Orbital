using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public GameObject indicatorPrefab;

    private GameObject[] currentIndicators;

    void Start()
    {
        currentIndicators = new GameObject[GameManager.instance.numberOfPlayers];
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            currentIndicators[0] = Instantiate(indicatorPrefab);
            currentIndicators[0].GetComponent<PlayerIndicator>().AssignPlayer(other.gameObject);

        }
        if (other.gameObject.CompareTag("Player2"))
        {
            currentIndicators[1] = Instantiate(indicatorPrefab);
            currentIndicators[1].GetComponent<PlayerIndicator>().AssignPlayer(other.gameObject);

        }
        if (other.gameObject.CompareTag("Player3"))
        {
            currentIndicators[2] = Instantiate(indicatorPrefab);
            currentIndicators[2].GetComponent<PlayerIndicator>().AssignPlayer(other.gameObject);

        }
        if (other.gameObject.CompareTag("Player4"))
        {
            currentIndicators[3] = Instantiate(indicatorPrefab);
            currentIndicators[3].GetComponent<PlayerIndicator>().AssignPlayer(other.gameObject);

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            Destroy(currentIndicators[0]);
        }
        if (other.gameObject.CompareTag("Player2"))
        {
            Destroy(currentIndicators[1]);
        }
        if (other.gameObject.CompareTag("Player3"))
        {
            Destroy(currentIndicators[2]);
        }
        if (other.gameObject.CompareTag("Player4"))
        {
            Destroy(currentIndicators[3]);
        }
    }
}