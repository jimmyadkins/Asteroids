using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wraparound : MonoBehaviour
{

    public float boundaryX = 155f;  // X-axis boundary
    public float boundaryZ = 75f;  // Z-axis boundary

    private TrailRenderer trailRenderer;
    private bool isPlayer = false;


    // Update is called once per frame
    void Update()
    {
        CheckBoundary();
    }

    void Start()
    {
        // Optionally check if this object is the player by checking tag or component
        if (CompareTag("Player"))  // Assuming the player has the tag "Player"
        {
            isPlayer = true;
            Transform trailTransform = transform.Find("Trail");
            if (trailTransform != null)
            {
                trailRenderer = trailTransform.GetComponent<TrailRenderer>();

                if (trailRenderer == null)
                {
                    Debug.LogWarning("No TrailRenderer found on 'Trail' child object.");
                }
            }
            else
            {
                Debug.LogWarning("No child object named 'Trail' found.");
            }
        }
    }

    void CheckBoundary()
    {
        Vector3 position = transform.position;

        // Check if ship has crossed the X boundary
        if (position.x > boundaryX)
        {
            if (isPlayer && trailRenderer != null)
            {
                trailRenderer.emitting = false;
                StartCoroutine(HandleTrailRenderer());
            }
            position.x = -boundaryX;

        }
        else if (position.x < -boundaryX)
        {
            if (isPlayer && trailRenderer != null)
            {
                trailRenderer.emitting = false;
                StartCoroutine(HandleTrailRenderer());
            }
            position.x = boundaryX; 

        }

        // Check if ship has crossed the Z boundary
        if (position.z > boundaryZ)
        {
            if (isPlayer && trailRenderer != null)
            {
                trailRenderer.emitting = false;
                StartCoroutine(HandleTrailRenderer());
            }
            position.z = -boundaryZ;
           
        }
        else if (position.z < -boundaryZ)
        {
            if (isPlayer && trailRenderer != null)
            {
                trailRenderer.emitting = false;
                StartCoroutine(HandleTrailRenderer());
            }
            position.z = boundaryZ;
           
        }

        transform.position = position;
    }

    IEnumerator HandleTrailRenderer()
    {
        // Disable the TrailRenderer
        trailRenderer.emitting = false;

        // Wait for a short duration before re-enabling (to avoid trails during the wrap)
        yield return new WaitForSeconds(0.5f);

        // Re-enable the TrailRenderer
        trailRenderer.emitting = true;
    }

}
