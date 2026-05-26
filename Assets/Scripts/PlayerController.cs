using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [Tooltip("How fast Terror moves")]
    public float moveSpeed = 4f;
    [Tooltip("Multiplier to add to Terror's speed while running")]
    public float runMult = 1.75f;
    [Tooltip("How high Terror jumps")]
    public float jumpHeight = 35f;
    [Tooltip("How fast Terror turns around")]
    public float rotationSpeed = 7f;

    [Header("References")]
    public Transform orientation;
    Rigidbody rb;

    Vector3 moveDirection;
    float moveHorizontalInput;
    float moveVerticalInput;
    float originalMoveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Stores the original speed (when not running)
        originalMoveSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void OnMove(InputValue input)
    {
        // Applies the direction from the input's Vector2 (x, z) to the input variables
        moveHorizontalInput = input.Get<Vector2>().x;
        moveVerticalInput = input.Get<Vector2>().y;
    }

    void OnRun(InputValue input)
    {
        if (input.isPressed)
        {
            // When the button is held, multiply the moveSpeed by it's multiplier
            moveSpeed *= runMult;
        }
        else
        {
            // When the button is released, set the moveSpeed to its original value
            moveSpeed = originalMoveSpeed;
        }
    }

    void OnJump(InputValue input)
    {
        // TODO Check if on ground
        if (input.isPressed)
        {
            // Adds a vertical (Vector3.up = new Vector(0, 1, 0)) force to Terror, making him jump
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    void MovePlayer()
    {
        // Calculate the moveDirection as Vector3
        //      orientation.forward/right are based on the camera position
        //      moveVerticalInput is 1 if up, -1 if down, 0 if release
        //      moveHorizontalInput is 1 if right, -1 if left, 0 if release
        moveDirection = orientation.forward * moveVerticalInput + orientation.right * moveHorizontalInput;

        if (moveDirection != Vector3.zero) // Add force only if Terror is actually moving
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 20f, ForceMode.Force);
            // Make Terror face turn towards the direction he is moving smoothly using Slerp
            transform.forward = Vector3.Slerp(transform.forward, moveDirection.normalized, Time.fixedDeltaTime * rotationSpeed);
        }

        // TODO     Make the capsule float
        // -            - Add a raycast under it
        // -            - Make it so that the raycast makes sure there's always a specific distance between the player and the ground
        // -            - Doing so will be useful for uneven grounds and slopes in the future
        // -        Add maximum speed & acceleration parameters
        // -            - Most important part is limiting the maximum velocity when sprinting and especially jumping
        // -            - Somehow check if it would be possible to lower the current force applied with moving when jumping or something that would have a similar effect
        // -        For the love of God, please don't overthink this whole thing, just make a basic movement system
    }
}
