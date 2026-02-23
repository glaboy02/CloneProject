
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PongPlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().y;
        rb.linearVelocity = new Vector2(0f, moveInput * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.CompareTag("Ball"))
        // {
        //     // Add logic for ball collision, e.g., play sound, increase score, etc.

        // }
    }

}
