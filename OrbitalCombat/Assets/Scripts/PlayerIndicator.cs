using UnityEngine;

public class PlayerIndicator : MonoBehaviour {
    private PlayerController player;
    private Transform icon => transform.GetChild(0);

    private float verticalBorder = 10.5f; //10.8f - 0.3f
    private float horizontalBorder = 18.9f; //19.2f - 0.3f

    private float verticalPosition = 9.8f; // 10.8f - 1.0f
    private float horizontalPosition = 18.2f; // 19.2f - 1.0f

    public void AssignPlayer(PlayerController givenPlayer)
    {
        player = givenPlayer;

        GetComponent<SpriteRenderer>().color = player.color;

        icon.GetComponent<SpriteRenderer>().sprite = player.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    void Update()
    {
        icon.rotation = player.transform.rotation;

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
