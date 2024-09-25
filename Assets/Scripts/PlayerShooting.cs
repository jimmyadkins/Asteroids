using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;  
    public Transform gunTipL;             
    public Transform gunTipR;
    public float projectileSpeed = 20f;
    public ParticleSystem gunMuzzleFlash;  
    public Rigidbody playerRigidbody;

    public float maxAmmo = 100f;
    public float currentAmmo;
    public float ammoDepletePerShot = 10f;
    public float ammoRegenRate = 20f;
    public float ammoRegenDelay = 3f;


    public float fireRate = 0.5f;
    private float nextFireTime = 0f; 
    private bool useLeftGunTip = true;
    private bool isFiring = false;
    private float lastShotTime;

    public Slider ammoSlider;

    void Start()
    {
        currentAmmo = maxAmmo;
        ammoSlider.maxValue = maxAmmo;
        ammoSlider.value = currentAmmo;
        if (gunMuzzleFlash != null)
        {
            gunMuzzleFlash.Stop();
        }
    }

    // Called by the Input System when the fire button is pressed or released
    public void OnFire(InputValue value)
    {
        isFiring = value.isPressed; // Set to true when the button is held down, false when released
    }

    void Update()
    {
        RegenerateAmmo();
        ammoSlider.value = currentAmmo;
        // Check if we're firing and if enough time has passed to shoot again
        if (isFiring && Time.time >= nextFireTime && currentAmmo > 0 && !PauseMenu.isPaused)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
            lastShotTime = Time.time;
        }
    }

    void Shoot()
    {
        currentAmmo -= ammoDepletePerShot;
        if (currentAmmo < 0) currentAmmo = 0;
        // Determine which gun tip to use
        Transform selectedGunTip = useLeftGunTip ? gunTipL : gunTipR;

        // Create the projectile
        GameObject projectile = Instantiate(projectilePrefab, selectedGunTip.position, selectedGunTip.rotation);

        // Apply velocity to the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = playerRigidbody.velocity + -selectedGunTip.up * projectileSpeed;

        // Play muzzle flash if available
        if (gunMuzzleFlash != null && !gunMuzzleFlash.isPlaying)
        {
            gunMuzzleFlash.Play();
        }

        // Toggle gun tip for alternating firing
        useLeftGunTip = !useLeftGunTip;
    }
    void RegenerateAmmo()
    {
        // Check if enough time has passed since the last shot
        if (Time.time >= lastShotTime + ammoRegenDelay)
        {
            // Regenerate ammo over time
            currentAmmo += ammoRegenRate * Time.deltaTime;
            if (currentAmmo > maxAmmo)
            {
                currentAmmo = maxAmmo; // Cap the ammo at max
            }
        }
    }
}
