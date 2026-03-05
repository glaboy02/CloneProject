using UnityEngine;

public class AsteroidsMoveDown : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private int scoreValue; // Score value for destroying this asteroid
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (GameManager.GameplayPaused) return;

        rb.linearVelocity = -(Vector2)transform.up * speed;
    }

    void Update()
    {
        if (GameManager.GameplayPaused)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }
        DestroyAsteroid();
    }

    private void DestroyAsteroid()
    {
        if (transform.position.y < -8f)
        {
            Debug.Log("Asteroid destroyed!");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            AsteroidsSpawner.Instance.DestroyAsteroid(gameObject);
            Instantiate(explosionEffect, transform.position, transform.rotation); // Trigger explosion effect

            Destroy(other.gameObject); // Destroy the bullet as well

            GameManager.Instance.IncreaseAsteroidsScore(scoreValue); // Add score for hitting the asteroid
        }
    }
}
