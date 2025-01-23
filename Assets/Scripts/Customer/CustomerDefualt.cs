using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDefault : MonoBehaviour
{
    public int points = 50;
    public Transform destination;
    public Collider interactionCollider; // Reference to the customer's collider
    private bool isPickedUp = false;

    public void PickUpCustomer()
    {
        if (destination == null)
        {
            destination = GameManager.instance.GetValidDestination(transform.position);
            if (destination == null)
            {
                Debug.LogError("No valid destinations available.");
                return;
            }
        }
        isPickedUp = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.zero;
            // Make the destination visible
            destination.gameObject.SetActive(true);
            // Update the UI objectives
            UIManager.instance.SetObjectives("Take customer to the destination: " + destination.name);
        }
        else
        {
            Debug.LogError("Player not found.");
        }
    }

    public void DropOffCustomer()
    {
        if (isPickedUp)
        {
            isPickedUp = false;
            transform.SetParent(null);
            UIManager.instance.AddPoints(points);
            UIManager.instance.SetObjectives("Bring customer to destination");
            Destroy(gameObject);
        }
    }

    public void Interact()
    {
        PickUpCustomer();
    }
}
