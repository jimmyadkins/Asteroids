using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;  
    public Transform gunTipL;             
    public Transform gunTipR;
    public float projectileSpeed = 20f;
    public ParticleSystem gunMuzzleFlash;  
    public Rigidbody playerRigidbody;


    public float fireRate = 0.5f;
    private float nextFireTime = 0f; 
    private bool useLeftGunTip = true;

    void Start()
    {
        if (gunMuzzleFlash != null)
        {
            gunMuzzleFlash.Stop();
        }
    }

    public void OnFire(InputValue value)
    {
        if (value.isPressed && Time.time >= nextFireTime) 
        {
            Shoot();
            nextFireTime = Time.time + fireRate;  
        }
    }

    void Shoot()
    {
        Transform selectedGunTip = useLeftGunTip ? gunTipL : gunTipR;
        GameObject projectile = Instantiate(projectilePrefab, selectedGunTip.position, selectedGunTip.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = playerRigidbody.velocity + -selectedGunTip.up * projectileSpeed;

        if (gunMuzzleFlash != null && !gunMuzzleFlash.isPlaying)
        {
            gunMuzzleFlash.Play();
        }

        useLeftGunTip = !useLeftGunTip;
    }
}
