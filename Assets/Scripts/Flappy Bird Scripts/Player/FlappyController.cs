using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FlappyController : MonoBehaviour
{

    [SerializeField] private float jumpForce = 5f;
    private Rigidbody2D rb;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.enabled = true;
        GameManager.Instance.GameStartFlappyBird();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            animator.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            GameManager.Instance.GameOverFlappyBird();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (GameManager.GameplayPaused) return;

        if (context.performed)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ScoreZone"))
        {
            GameManager.Instance.IncreaseFlappyBirdScore();
        }
    }
}
