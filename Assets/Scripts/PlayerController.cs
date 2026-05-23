using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [Tooltip("How fast the player moves")]
    public float moveSpeed = 5f;
    [Tooltip("How high the player jumps")]
    public float jumpHeight = 50f;

    Rigidbody rb;
    Vector3 moveDirection;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        // Constantly checks for change in moveDirection to apply to the player
        rb.MovePosition(rb.transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue input) {
        // Applies the direction from the input's Vector2 (x, z) to moveDirection
        moveDirection = new Vector3(input.Get<Vector2>().x, 0, input.Get<Vector2>().y);
    }

    void OnJump(InputValue input) {
        if (input.isPressed) // When the button is pressed
            // Adds a vertical (Vector3.up = new Vector(0, 1, 0)) force to the player, making them jump
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

    void OnSprint(InputValue input) {
        if (input.isPressed)
            Debug.Log("Sprint");
    }
}
