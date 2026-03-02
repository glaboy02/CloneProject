using UnityEngine;
using System.Collections;

public class AsteroidsSpawner : MonoBehaviour
{
    public static AsteroidsSpawner Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject[] spawnPoints;

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

    private void SpawnAsteroids(int index, bool firstAsteroid, Vector3 position, Quaternion rotation)
    {
        if (firstAsteroid)
        {
            Transform spawnTransform = RandomSpawnPosition();
            Instantiate(GetAsteroidIndex(index), spawnTransform.position, spawnTransform.rotation);
        }
        else
        {
            Instantiate(GetAsteroidIndex(index), position, rotation);
        }
    }

    private Transform RandomSpawnPosition()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[spawnIndex].transform;
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
            SpawnAsteroids(1, false, asteroid.transform.position, asteroid.transform.rotation); // Spawn large asteroids
            Destroy(asteroid);
        }
        else if (asteroid.CompareTag("LargeAsteroid"))
        {
            // Handle Large asteroid destruction logic
            SpawnAsteroids(2, false, asteroid.transform.position, asteroid.transform.rotation); // Spawn medium asteroids
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
            SpawnAsteroids(0, true, Vector3.zero, Quaternion.identity); // Spawn large asteroids
            yield return new WaitForSeconds(delay);
        }
    }


}
