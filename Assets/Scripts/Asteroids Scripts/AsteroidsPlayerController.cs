using UnityEngine;
using UnityEngine.InputSystem;

public class AsteroidsPlayerController : MonoBehaviour
{

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float moveInputY;
    private float moveInputX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(InputAction.CallbackContext context)
    {
        if (GameManager.GameplayPaused) return;

        moveInputY = context.ReadValue<Vector2>().y;
        moveInputX = context.ReadValue<Vector2>().x;
        rb.linearVelocity = new Vector2(moveInputX, moveInputY) * moveSpeed;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (GameManager.GameplayPaused) return;
        // Implement firing logic here
        if (context.performed)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }


    }

}