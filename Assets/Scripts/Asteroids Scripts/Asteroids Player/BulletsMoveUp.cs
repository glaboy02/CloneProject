using UnityEngine;

public class BulletsMoveUp : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.linearVelocity = -(Vector2)transform.up * speed;

    }

    // Update is called once per frame
    void Update()
    {
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        // if (transform.position.y > 8f) // Adjust this value based on your game's needs
        // {
        //     Destroy(gameObject);
        // }

        Vector3 pos = transform.position;
        if (pos.x > 9f || pos.x < -9f || pos.y > 5f || pos.y < -4f)
        {
            Destroy(gameObject);
        }
    }

    public void ShootBullet(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
    }
}
