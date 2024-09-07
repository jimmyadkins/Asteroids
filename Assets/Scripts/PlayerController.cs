using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public GameObject thrustMesh;
    public float thrustForce = 100f;      
    public float rotationSpeed = 10000f;
    public float maxSpeed = 1000f; 

    private float rotateInput;
    private bool isThrusting = false;

    public GameObject explosionEffectPrefab;

    //public float boundaryX = 155f;  // X-axis boundary
    //public float boundaryZ = 75f;  // Z-axis boundary

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnRotate(InputValue value)
    {
        rotateInput = value.Get<float>();
    }

    public void OnThrust(InputValue value)
    {
        isThrusting = value.isPressed;

        if (isThrusting)
        {
            thrustMesh.SetActive(true);  // Show the thrust mesh
        }
        else
        {
            thrustMesh.SetActive(false);  // Hide the thrust mesh
        }
    }



    void Update()
    {
        float rotation = rotateInput * rotationSpeed * Time.deltaTime;
        //rb.AddTorque(Vector3.up * rotation);  // Rotate around the Y-axis

        transform.Rotate(Vector3.forward * rotation);  // Manually rotate around the Y-axis
    
        //else
        //{
        //    // Reset the angular velocity when no rotation input is given
        //    rb.angularVelocity = Vector3.zero;
        //}
    }

    void FixedUpdate()
    {
        if (isThrusting)
        {
            Debug.Log("IsThrusting");
            Vector3 force = transform.up * -thrustForce;

            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(force, ForceMode.Acceleration);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }

}
