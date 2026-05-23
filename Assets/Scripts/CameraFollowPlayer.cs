using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Tooltip("Vertical offset to Terror")]
    public float offsetY = 1f;
    [Tooltip("Horizontal offset to Terror\nHas to be negative to be behind him")]
    public float offsetZ = -3.5f;

    GameObject player;
    Vector3 playerPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        playerPosition = player.transform.position;

        // Moves the camera according to Terror's position (and adds the offset set in the public variables)
        transform.position = new Vector3(playerPosition.x, playerPosition.y + offsetY, playerPosition.z + offsetZ);
    }
}
