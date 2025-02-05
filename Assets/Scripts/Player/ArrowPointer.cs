using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public Transform car; // Reference to the car
    public Transform arrow; // Reference to the arrow model
    private Transform destination; // Reference to the destination

    void Update()
    {
        if (destination != null)
        {
            // Make the arrow visible
            arrow.gameObject.SetActive(true);

            // Calculate the direction to the destination
            Vector3 direction = destination.position - car.position;
            direction.y = 0; // Keep the arrow horizontal

            // Rotate the arrow to point towards the destination
            arrow.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            // Hide the arrow when no destination is set
            arrow.gameObject.SetActive(false);
        }
    }

    public void SetDestination(Transform newDestination)
    {
        destination = newDestination;
    }

    public void ClearDestination()
    {
        destination = null;
    }
}