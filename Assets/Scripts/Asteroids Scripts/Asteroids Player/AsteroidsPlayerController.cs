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
        FixedBoundary();
    }

    private void FixedBoundary()
    {
        if (transform.position.x > 10f)
        {
            transform.position = new Vector3(-10f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -10f)
        {
            transform.position = new Vector3(10f, transform.position.y, transform.position.z);
        }
        if (transform.position.y < -4f)
        {
            transform.position = new Vector3(transform.position.x, -4f, transform.position.z);
        }
        else if (transform.position.y > 5f)
        {
            transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
        }

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