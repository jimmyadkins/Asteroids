using UnityEngine;

public class BoundaryDestroyer : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        // Check if the object that exited the trigger is a bullet (tagged as Bullet)
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
