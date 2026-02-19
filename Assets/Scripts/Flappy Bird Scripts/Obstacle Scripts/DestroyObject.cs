using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -13f)
        {
            Destroy(gameObject);
        }
    }


}
