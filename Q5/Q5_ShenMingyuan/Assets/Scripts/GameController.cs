using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    // Outlets
    public Transform[] spawnPoints;
    public GameObject[] asteroidPrefabs;
    public GameObject explosionPrefab;

    // Configuration
    public float minAsteroidDelay = 0.2f;
    public float maxAsteroidDelay = 2f;

    // State Tracking
    public float timeElapsed;
    public float asteroidDelay;


    // Methods
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("AsteroidSpawnTimer");
    }

    // Update is called once per frame
    void Update()
    {
        // Increment passage of time for each frame of the game
        timeElapsed += Time.deltaTime;

        // Compute Asteroid delay
        float decreaseDelayOverTime = maxAsteroidDelay - ((maxAsteroidDelay - minAsteroidDelay) / 30f * timeElapsed);
        asteroidDelay = Mathf.Clamp(decreaseDelayOverTime, minAsteroidDelay, maxAsteroidDelay);

    }

    void SpawnAsteroid()
    {
        // Pick random spawn points and prefabs
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject randomAsteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Spawn
        Instantiate(randomAsteroidPrefab, randomSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator AsteroidSpawnTimer()
    {
        // Wait
        yield return new WaitForSeconds(asteroidDelay);

        // Spawn
        SpawnAsteroid();

        // Repeat
        StartCoroutine("AsteroidSpawnTimer");
    }
}
