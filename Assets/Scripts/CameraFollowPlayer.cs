using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
    public float offsetY = 1f;
    public float offsetZ = -3.5f;
    
    private GameObject player;
    private Vector3 playerPosition;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        playerPosition = player.transform.position;

        transform.position = new Vector3(playerPosition.x, playerPosition.y + offsetY, playerPosition.z + offsetZ);
    }
}
