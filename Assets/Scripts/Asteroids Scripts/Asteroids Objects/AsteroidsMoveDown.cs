using UnityEngine;

public class AsteroidsMoveDown : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private ParticleSystem explosionEffect;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (GameManager.GameplayPaused) return;
        // rb.linearVelocity = Vector2.down * speed;
        int xPosition = (int)gameObject.transform.position.x;
        int yPosition = (int)gameObject.transform.position.y;
        // if (yPosition < 0 && xPosition > 0)
        // {
        //     Debug.Log("Asteroid moving down and to the left");
        //     rb.linearVelocity = -(Vector2)transform.right * speed;
        // }
        rb.linearVelocity = -(Vector2)transform.up * speed;
    }

    // Update is called once per frame
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
        if (transform.position.y < -8f) // Adjust this value based on your game's needs
        {
            Debug.Log("Asteroid destroyed!");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Asteroid hit!");
            AsteroidsSpawner.Instance.DestroyAsteroid(gameObject);
            Instantiate(explosionEffect, transform.position, transform.rotation); // Trigger explosion effect

            Destroy(other.gameObject); // Destroy the bullet as well
        }
    }
}
