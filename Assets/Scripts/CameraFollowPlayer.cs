using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
    [Tooltip("Vertical offset to the player")]
    public float offsetY = 1f;
    [Tooltip("Horizontal offset to the player\nHas to be negative to be behind the player")]
    public float offsetZ = -3.5f;

    GameObject player;
    Vector3 playerPosition;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        playerPosition = player.transform.position;

        // Moves the camera according to the player's position (and adds the offset set in the public variables)
        transform.position = new Vector3(playerPosition.x, playerPosition.y + offsetY, playerPosition.z + offsetZ);
    }
}
