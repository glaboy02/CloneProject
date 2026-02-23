using UnityEngine;
using System.Collections;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    private int minSpawn = 4;
    private int maxSpawn = 10;
    private int spawnX = 10;
    private int spawnZ = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnObstacleWithDelay(3f));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameplayPaused)
        {
            StopAllCoroutines();
        }
    }

    public void SpawnObstacle()
    {
        Instantiate(obstacle, GetRandomVectorSpawn(spawnX, minSpawn, maxSpawn, spawnZ), Quaternion.identity);
    }

    // public int GetRandomObjectIndex()
    // {
    //     index = Random.Range(0, obstacles.Length);
    //     return index;
    // }

    public Vector3 GetRandomVectorSpawn(int X, int minY, int maxY, int Z)
    {
        return new Vector3(X, GetRandomNumber(minY, maxY), Z);
    }

    private int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    private IEnumerator SpawnObstacleWithDelay(float delay)
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(delay);
        }
    }
}
