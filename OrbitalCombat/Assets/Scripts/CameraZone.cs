using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour {
    public GameObject indicatorPrefab;

    private Dictionary<PlayerController, PlayerIndicator> currentIndicators = new Dictionary<PlayerController, PlayerIndicator>();

    private void OnTriggerExit2D(Collider2D other) {
        var player = other.GetComponent<PlayerController>();
        if (player && !currentIndicators.ContainsKey(player)) {
            currentIndicators[player] = Instantiate(indicatorPrefab).GetComponent<PlayerIndicator>();
            currentIndicators[player].AssignPlayer(player);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<PlayerController>();
        if (player && currentIndicators.ContainsKey(player)) {
            Destroy(currentIndicators[player].gameObject);
            currentIndicators.Remove(player);
        }
    }
}