using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 2.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerFlappy.GameplayPaused) return;

        transform.position += Vector3.left * Time.deltaTime * speed;
    }
}
