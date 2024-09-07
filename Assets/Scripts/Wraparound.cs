using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wraparound : MonoBehaviour
{

    public float boundaryX = 155f;  // X-axis boundary
    public float boundaryZ = 75f;  // Z-axis boundary


    // Update is called once per frame
    void Update()
    {
        CheckBoundary();
    }

    void CheckBoundary()
    {
        Vector3 position = transform.position;

        // Check if ship has crossed the X boundary
        if (position.x > boundaryX)
        {
            position.x = -boundaryX;
        }
        else if (position.x < -boundaryX)
        {
            position.x = boundaryX;
        }

        // Check if ship has crossed the Z boundary
        if (position.z > boundaryZ)
        {
            position.z = -boundaryZ;
        }
        else if (position.z < -boundaryZ)
        {
            position.z = boundaryZ;
        }

        transform.position = position;
    }

}
