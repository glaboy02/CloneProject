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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOverPanel.SetActive(false); // Hide the game over panel at the start
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

}