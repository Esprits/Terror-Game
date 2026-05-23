using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    Rigidbody rb;

    public float movementSpeed = 5f;

    Vector3 direction;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.transform.position + direction * movementSpeed * Time.fixedDeltaTime);
    }

    private void OnMove(InputValue input) {
        direction = new Vector3(input.Get<Vector2>().x, 0, input.Get<Vector2>().y);
    }

    private void OnJump(InputValue input) {
        if (input.isPressed)
            Debug.Log("Jump");
    }

    private void OnSprint(InputValue input) {
        if (input.isPressed)
            Debug.Log("Sprint");
    }
}
