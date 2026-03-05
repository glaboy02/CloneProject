
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PongPlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string actionMapName;  // "Player1" or "Player2"
    [SerializeField] private string actionName;     // "Move1" or "Move2"

    private Rigidbody2D rb;
    private InputAction moveAction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        var map = inputActions.FindActionMap(actionMapName, throwIfNotFound: true);
        moveAction = map.FindAction(actionName, throwIfNotFound: true);
        moveAction.Enable();
    }

    void Update()
    {
        if (GameManager.GameplayPaused) return;

        float moveInput = moveAction.ReadValue<Vector2>().y;
        rb.linearVelocity = new Vector2(0f, moveInput * moveSpeed);
    }

    void OnDestroy()
    {
        moveAction?.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    // }

    // public void Move(InputAction.CallbackContext context)
    // {
    //     if (GameManager.GameplayPaused) return;

    //     moveInput = context.ReadValue<Vector2>().y;
    //     rb.linearVelocity = new Vector2(0f, moveInput * moveSpeed);
    // }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     // if (other.gameObject.CompareTag("Ball"))
    //     // {
    //     //     // Add logic for ball collision, e.g., play sound, increase score, etc.

    //     // }
    // }

}
