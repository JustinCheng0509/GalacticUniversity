using UnityEngine;
using UnityEngine.InputSystem;

public class OverworldMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector2 moveDirection;

    public InputActionReference move;

    private void Update() {
        moveDirection = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);     
    }
}
