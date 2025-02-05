using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDefault : MonoBehaviour
{
    public int points = 50;
    public Transform destination;
    public Collider interactionCollider;
    private bool isPickedUp = false;
    private ArrowPointer arrowController;

    void Start()
    {
        arrowController = FindObjectOfType<ArrowPointer>();
    }

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
            destination.gameObject.SetActive(true);
            UIManager.instance.SetObjectives("Take customer to the destination: " + destination.name);
            arrowController.SetDestination(destination); // Set the destination on the arrow
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
            arrowController.ClearDestination(); // Clear the destination on the arrow
            Destroy(gameObject);
        }
    }

    public void Interact()
    {
        PickUpCustomer();
    }
}
