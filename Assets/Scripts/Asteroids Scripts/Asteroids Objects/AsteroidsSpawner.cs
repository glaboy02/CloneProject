using UnityEngine;
using System.Collections;

public class AsteroidsSpawner : MonoBehaviour
{
    public static AsteroidsSpawner Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject[] asteroidPrefab;
    [SerializeField] private float spawnInterval = 5f;
    private float xRange = 9f;
    private int ySpawn = 9;
    // private int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnAsteroidsWithDelay(spawnInterval));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnAsteroids(int index, bool firstAsteroid, Vector3 position)
    {
        if (firstAsteroid)
        {
            Instantiate(GetAsteroidIndex(index), RandomSpawnPosition(), Quaternion.identity);
        }
        else
        {
            Instantiate(GetAsteroidIndex(index), position, Quaternion.identity);
        }
    }

    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawn, 0);
    }

    private GameObject GetAsteroidIndex(int index)
    {
        return asteroidPrefab[index];
    }

    public void DestroyAsteroid(GameObject asteroid)
    {
        if (asteroid.CompareTag("HugeAsteroid"))
        {
            // Handle Huge asteroid destruction logic
            SpawnAsteroids(1, false, asteroid.transform.position); // Spawn large asteroids
            Destroy(asteroid);
        }
        else if (asteroid.CompareTag("LargeAsteroid"))
        {
            // Handle Large asteroid destruction logic
            SpawnAsteroids(2, false, asteroid.transform.position); // Spawn medium asteroids
            Destroy(asteroid);
        }
        else if (asteroid.CompareTag("MediumAsteroid"))
        {
            // Handle Medium asteroid destruction logic
            // Spawn small asteroids
            Destroy(asteroid);
        }
        else if (asteroid.CompareTag("SmallAsteroid"))
        {
            // Handle Small asteroid destruction logic
            Destroy(asteroid);
        }
    }

    IEnumerator SpawnAsteroidsWithDelay(float delay)
    {
        while (GameManager.GameplayPaused == false)
        {
            SpawnAsteroids(0, true, Vector3.zero); // Spawn large asteroids
            yield return new WaitForSeconds(delay);
        }
    }


}
