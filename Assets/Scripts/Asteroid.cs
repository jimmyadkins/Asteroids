using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject asteroidPrefab;      
    public GameObject hitEffectPrefab;     
    public GameObject destructionEffectPrefab; 

    public float splitSpeed = 15f;         

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
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
            }
        }

        Destroy(gameObject);
    }
}
