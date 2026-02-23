using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 lastVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
    }
    private void FixedUpdate()
    {
        lastVelocity = rb.linearVelocity;
    }

    private void LaunchBall()
    {
        float xDirection = Random.Range(0, 2) == 0 ? -1 : 1; // Randomly choose left or right
        float yDirection = Random.Range(-1f, 1f); // Random vertical direction
        Vector2 launchDirection = new Vector2(xDirection, yDirection).normalized;
        rb.linearVelocity = launchDirection * initialSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Reflect the ball's velocity based on the collision normal
            Vector2 normal = other.contacts[0].normal;
            rb.linearVelocity = Vector2.Reflect(lastVelocity, normal) * 1.1f; // Increase speed by 10% on each hit
        }

        if (other.gameObject.CompareTag("ScoreZone"))
        {
            
        }
    }
}
