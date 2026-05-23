using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [Tooltip("How fast Terror moves")]
    public float moveSpeed = 4f;
    [Tooltip("Multiplier to add to Terror's speed while running")]
    public float runMult = 1.75f;
    [Tooltip("How high Terror jumps")]
    public float jumpHeight = 35f;

    Rigidbody rb;
    Vector3 moveDirection;
    float originalMoveSpeed;

    void Start() {
        rb = GetComponent<Rigidbody>();

        // Stores the original speed (when not running)
        originalMoveSpeed = moveSpeed;
    }

    void FixedUpdate() {
        // Constantly checks for change in moveDirection to apply to Terror
        rb.MovePosition(rb.transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue input) {
        // Applies the direction from the input's Vector2 (x, z) to moveDirection
        moveDirection = new Vector3(input.Get<Vector2>().x, 0, input.Get<Vector2>().y);
    }

    void OnRun(InputValue input) {
        if (input.isPressed)
            // When the button is held, multiply the moveSpeed by it's multiplier
            moveSpeed *= runMult;
        else
            // When the button is released, set the moveSpeed to its original value
            moveSpeed = originalMoveSpeed;
    }

    void OnJump(InputValue input) {
        if (input.isPressed) // When the button is pressed
            // Adds a vertical (Vector3.up = new Vector(0, 1, 0)) force to Terror, making him jump
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
}
