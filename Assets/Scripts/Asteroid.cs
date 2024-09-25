using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject asteroidPrefab;      
    public GameObject hitEffectPrefab;     
    public GameObject destructionEffectPrefab; 

    public float splitSpeed = 15f;         

    private Rigidbody rb;
    public enum AsteroidSize { Large, Medium, Small }
    public AsteroidSize asteroidSize;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Asteroid asteroid = GetComponent<Asteroid>();
            if (asteroid.asteroidSize == Asteroid.AsteroidSize.Large)
            {
                scoreManager.AddScore(scoreManager.largeAsteroidPoints);
            }
            else if (asteroid.asteroidSize == Asteroid.AsteroidSize.Medium)
            {
                scoreManager.AddScore(scoreManager.mediumAsteroidPoints);
            }
            else if (asteroid.asteroidSize == Asteroid.AsteroidSize.Small)
            {
                scoreManager.AddScore(scoreManager.smallAsteroidPoints);
            }
            Instantiate(hitEffectPrefab, collision.contacts[0].point, Quaternion.identity);

            Destroy(collision.gameObject);

            DestroyAsteroid();
        }
    }

    void DestroyAsteroid()
    {
        Instantiate(destructionEffectPrefab, transform.position, Quaternion.identity);

        if (transform.localScale.x > 500f) 
        {
            // Spawn two smaller asteroids
            for (int i = 0; i < 2; i++)
            {
                GameObject smallerAsteroid = Instantiate(asteroidPrefab, transform.position, Quaternion.identity);

                smallerAsteroid.transform.localScale = transform.localScale * 0.5f;

                // Apply random velocity to the new smaller asteroids
                Rigidbody smallRb = smallerAsteroid.GetComponent<Rigidbody>();
                Vector3 randomDirection = Random.insideUnitSphere.normalized;
                smallRb.velocity = randomDirection * splitSpeed;

                Asteroid asteroidComponent = smallerAsteroid.GetComponent<Asteroid>();

                if (transform.localScale.x > 250f)  // If it's medium size after split
                {
                    asteroidComponent.asteroidSize = Asteroid.AsteroidSize.Medium;
                }
                else  // If it's small size after split
                {
                    asteroidComponent.asteroidSize = Asteroid.AsteroidSize.Small;
                }
            }
        }

        Destroy(gameObject);
    }
}
