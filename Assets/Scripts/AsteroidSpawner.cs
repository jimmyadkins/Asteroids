using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public float spawnRadius = 200f;
    public int initialAsteroidCount = 10;  // Number of asteroids to spawn at start
    public float spawnInterval = 5f;       // Time interval between spawns in seconds
    public Camera mainCamera;

    // Speeds for different asteroid sizes
    public float largeAsteroidSpeed = 5f;
    public float mediumAsteroidSpeed = 10f;
    public float smallAsteroidSpeed = 15f;

    // Masses for different asteroid sizes
    public float largeAsteroidMass = 20f;
    public float mediumAsteroidMass = 10f;
    public float smallAsteroidMass = 5f;

    void Start()
    {
        SpawnInitialAsteroids();
        StartCoroutine(SpawnAsteroidsContinuously());
    }

    void SpawnInitialAsteroids()
    {
        for (int i = 0; i < initialAsteroidCount; i++)
        {
            SpawnAsteroid();
        }
    }

    IEnumerator SpawnAsteroidsContinuously()
    {
        while (true)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(spawnInterval);  // Wait for the next spawn
        }
    }

    void SpawnAsteroid()
    {
        // Pick a random asteroid prefab
        int randomIndex = Random.Range(0, asteroidPrefabs.Length);
        GameObject asteroid = Instantiate(asteroidPrefabs[randomIndex]);

        Vector3 spawnPosition = GetSpawnPositionOutsideScreen();
        asteroid.transform.position = spawnPosition;

        // Determine the asteroid size (Large, Medium, Small)
        float randomSize = Random.Range(0f, 1f);
        float asteroidScale = 1f;
        float asteroidSpeed = 0f;
        float asteroidMass = 0f;

        // Assign size, speed, and mass based on random selection
        if (randomSize < 0.33f)  // Large asteroid
        {
            asteroidScale = 1f;
            asteroidSpeed = largeAsteroidSpeed;
            asteroidMass = largeAsteroidMass;
            asteroid.GetComponent<Asteroid>().asteroidSize = Asteroid.AsteroidSize.Large;
        }
        else if (randomSize < 0.66f)  // Medium asteroid
        {
            asteroidScale = 0.5f;
            asteroidSpeed = mediumAsteroidSpeed;
            asteroidMass = mediumAsteroidMass;
            asteroid.GetComponent<Asteroid>().asteroidSize = Asteroid.AsteroidSize.Medium;
        }
        else  // Small asteroid
        {
            asteroidScale = 0.25f;
            asteroidSpeed = smallAsteroidSpeed;
            asteroidMass = smallAsteroidMass;
            asteroid.GetComponent<Asteroid>().asteroidSize = Asteroid.AsteroidSize.Small;
        }

        asteroid.transform.localScale *= asteroidScale;

        Rigidbody rb = asteroid.GetComponent<Rigidbody>();
        rb.mass = asteroidMass;

        Vector3 targetPosition = GetRandomPositionInScreenSpace();
        Vector3 directionToCenter = (targetPosition - spawnPosition).normalized;

        rb.velocity = directionToCenter * asteroidSpeed;
    }

    Vector3 GetSpawnPositionOutsideScreen()
    {
        // Choose a random side of the screen to spawn from (left, right, top, bottom)
        int side = Random.Range(0, 4);

        Vector3 spawnPosition = Vector3.zero;

        float screenZ = mainCamera.farClipPlane * 0.5f;
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, screenZ));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, screenZ));

        float spawnOffset = 20f;  // How far outside the screen to spawn

        switch (side)
        {
            case 0: // Left
                spawnPosition = new Vector3(bottomLeft.x - spawnOffset, 0, Random.Range(bottomLeft.z, topRight.z));
                break;
            case 1: // Right
                spawnPosition = new Vector3(topRight.x + spawnOffset, 0, Random.Range(bottomLeft.z, topRight.z));
                break;
            case 2: // Bottom
                spawnPosition = new Vector3(Random.Range(bottomLeft.x, topRight.x), 0, bottomLeft.z - spawnOffset);
                break;
            case 3: // Top
                spawnPosition = new Vector3(Random.Range(bottomLeft.x, topRight.x), 0, topRight.z + spawnOffset);
                break;
        }

        return spawnPosition;
    }

    Vector3 GetRandomPositionInScreenSpace()
    {
        // Get a random point inside the screen space at a suitable depth
        float screenZ = mainCamera.farClipPlane * 0.5f; // Adjust the depth if necessary
        Vector3 screenPosition = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), screenZ);
        return mainCamera.ViewportToWorldPoint(screenPosition);
    }
}
