using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [Tooltip("How fast Terror moves")]
    public float moveSpeed;
    [Tooltip("Multiplier to add to Terror's speed while running")]
    public float runMult;
    [Tooltip("How high Terror jumps")]
    public float jumpHeight;
    [Tooltip("How fast Terror turns around")]
    public float rotationSpeed;
    [Tooltip("How fast Terror stops when not moving anymore")]
    public float groundDrag;
    [Tooltip("Terror's height")]
    public float playerHeight;

    [Header("References")]
    public Transform orientation;
    public LayerMask groundLayer;
    Rigidbody rb;

    Vector3 moveDirection;
    float moveHorizontalInput;
    float moveVerticalInput;
    float originalMoveSpeed;
    bool onGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Stores the original speed (when not running)
        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        GameObject.Find("Velocity").GetComponent<TextMeshProUGUI>().text = "Velocity: " + rb.linearVelocity.magnitude;
        LimitVelocity();

        onGround = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, playerHeight * 0.5f + 0.2f, groundLayer);
        Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.2f), Color.red); // Show Raycast

        if (onGround)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
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
        if (input.isPressed && onGround)
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
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            // Make Terror face turn towards the direction he is moving smoothly using Slerp
            transform.forward = Vector3.Slerp(transform.forward, moveDirection.normalized, Time.fixedDeltaTime * rotationSpeed);
        }
    }

    void LimitVelocity()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            // Reset the velocity by normalising it and then multiplying it by what the move speed is supposed to be
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
