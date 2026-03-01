using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class AsteroidsPlayerController : MonoBehaviour
{

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private ParticleSystem explosionEffect;
    private Rigidbody2D rb;
    private float moveInputY;
    private float moveInputX;
    public Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cam == null)
        {
            // Find the main camera if not assigned
            cam = Camera.main;
        }

        rb = GetComponent<Rigidbody2D>();
        gameOverPanel.SetActive(false); // Hide the game over panel at the start
    }

    // Update is called once per frame
    void Update()
    {
        FixedBoundary();

    }

    private void FixedUpdate()
    {
        RotateCharacter();
    }

    private void FixedBoundary()
    {
        if (transform.position.x > 8f)
        {
            transform.position = new Vector3(8f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -8f)
        {
            transform.position = new Vector3(-8f, transform.position.y, transform.position.z);
        }
        if (transform.position.y < -3.5f)
        {
            transform.position = new Vector3(transform.position.x, -3.5f, transform.position.z);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("HugeAsteroid") || other.gameObject.CompareTag("LargeAsteroid") || other.gameObject.CompareTag("MediumAsteroid"))
        {
            Debug.Log("Player hit!");
            // Implement player hit logic here (e.g., reduce health, trigger explosion, etc.)
            Instantiate(explosionEffect, transform.position, transform.rotation); // Trigger explosion effect
            GameManager.SetGamePaused(true); // Pause the game when the player is hit
            gameOverPanel.SetActive(true); // Show the game over panel
            Destroy(gameObject); // Destroy the player object
        }

    }

    void RotateCharacter()
    {
        // 1. Get the mouse position in screen coordinates
        Vector3 mousePos = Mouse.current.position.ReadValue(); // Get mouse position from the new Input System

        // 2. Convert the screen position to a world position
        // The z distance is important for a perspective camera, but less critical for orthographic 2D
        mousePos.z = transform.position.z - cam.transform.position.z; // Adjust Z based on your game setup
        Vector3 worldMousePos = cam.ScreenToWorldPoint(mousePos);

        // 3. Calculate the direction from the character to the mouse
        Vector3 direction = worldMousePos - transform.position;

        // 4. Determine the angle in degrees using Mathf.Atan2
        // Mathf.Atan2 returns the angle in radians, so convert to degrees
        // Use Vector2 for 2D specific calculations
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 5. Apply the rotation to the character
        // Quaternion.Euler is used to set the rotation using Euler angles (pitch, yaw, roll)
        // In 2D, we typically rotate around the Z-axis
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));

        // If your sprite naturally points "up" (along the Y-axis) instead of "right" (along the X-axis), you may need to add or subtract 90 degrees:
        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); 
    }

}