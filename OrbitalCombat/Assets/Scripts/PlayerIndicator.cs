using UnityEngine;

public class PlayerIndicator : MonoBehaviour
{
    private PlayerController player;

    private float verticalBorder = 10.5f; //10.8f - 0.3f
    private float horizontalBorder = 18.9f; //19.2f - 0.3f

    private float verticalPosition = 10.2f; // 10.8f - 0.6f
    private float horizontalPosition = 18.6f; // 19.2f - 0.6f

    public void AssignPlayer(PlayerController givenPlayer)
    {
        player = givenPlayer;

        GetComponent<SpriteRenderer>().color = player.color;
    }

    void Update()
    {
        if (player.transform.position.y > verticalBorder)
        {
            transform.position = new Vector2(
                Mathf.Clamp(player.transform.position.x, -horizontalPosition, horizontalPosition),
                verticalPosition);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
        }
        else if (player.transform.position.y < -verticalBorder)
        {
            transform.position = new Vector2(
                Mathf.Clamp(player.transform.position.x, -horizontalPosition, horizontalPosition),
                -verticalPosition);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else if (player.transform.position.x > horizontalBorder)
        {
            transform.position = new Vector2(
                horizontalPosition,
                Mathf.Clamp(player.transform.position.y, -verticalPosition, verticalPosition));
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
        }
        else if (player.transform.position.x < -horizontalBorder)
        {
            transform.position = new Vector2(
                -horizontalPosition,
                Mathf.Clamp(player.transform.position.y, -verticalPosition, verticalPosition));
            transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
        }
    }
}
