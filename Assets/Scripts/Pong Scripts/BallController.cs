using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float speedIncreaseMultiplier = 1.05f;
    [SerializeField] private float launchDelay = 2.5f;
    private Rigidbody2D rb;
    private Vector2 lastVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(LaunchBallWithDelay());
    }
    private void FixedUpdate()
    {
        if (GameManager.GameplayPaused)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }
        rb.constraints = RigidbodyConstraints2D.None;
        lastVelocity = rb.linearVelocity;

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
    
    private IEnumerator LaunchBallWithDelay()
    {
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(launchDelay);

        float xDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        float yDirection = Random.Range(-1f, 1f);
        Vector2 launchDirection = new Vector2(xDirection, yDirection).normalized;
        rb.linearVelocity = launchDirection * initialSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Reflect the ball's velocity based on the collision normal
            Vector2 normal = other.contacts[0].normal;
            Vector2 newVelocity = Vector2.Reflect(lastVelocity, normal) * speedIncreaseMultiplier;

            // Clamp to max speed
            if (newVelocity.magnitude > maxSpeed)
            {
                newVelocity = newVelocity.normalized * maxSpeed;
            }

            rb.linearVelocity = newVelocity; // Increase speed by 10% on each hit
        }

        if (other.gameObject.CompareTag("ScoreZone"))
        {
            GameManager.Instance.IncreasePongScore(other.gameObject.name);
            ResetBall();
        }
    }
    private void ResetBall()
    {
        transform.position = new Vector3(0, 0.5f, 0);
        rb.linearVelocity = Vector2.zero;
        StartCoroutine(LaunchBallWithDelay());
    }
}
