using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDefault : MonoBehaviour
{
    public int points = 50;
    public Transform currentDestination;
    public Transform scenicRoute;
    public Collider interactionCollider;
    private bool isPickedUp = false;
    private ArrowPointer arrowController;
    public Renderer sphereRenderer;

    void Start()
    {
        arrowController = FindObjectOfType<ArrowPointer>();
    }

    public void PickUpCustomer()
    {
        if (currentDestination == null)
        {
            currentDestination = GameManager.instance.GetValidDestination(transform.position);
            if (currentDestination == null)
            {
                Debug.LogError("No valid destinations available.");
                return;
            }
        }
        isPickedUp = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            //transform.SetParent(player.transform);
            //transform.localPosition = Vector3.zero;
            
            object.GameObject.GetComponent<MeshRenderer>().enabled = false; //CHANGE WHEN ANIMATION IS MADE!!
            currentDestination.gameObject.SetActive(true);
            UIManager.instance.SetObjectives("Take customer to the destination: " + currentDestination.name);
            arrowController.SetDestination(currentDestination); // Sets the destination on the arrow
            if (sphereRenderer != null)
            {
                sphereRenderer.enabled = false;
            }
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
            if (sphereRenderer != null) //Customer Sphere disappears when picked up
            {
                sphereRenderer.enabled = true;
            }
            if (currentDestination != null)
            {
                currentDestination.gameObject.SetActive(false);
            }
            Destroy(gameObject);
        }
    }

    public void Interact()
    {
        PickUpCustomer();
        //SetTipObjective();
    }

    //public void SetTipObjective()
    //{
    //    int objectiveType = 1;//Random.Range(1, 4);
    //    switch (objectiveType)
    //    {
    //        case 1:
    //            {
    //                //Scenic
    //                scenicRoute = GameManager.instance.GetValidScenic(transform.position);
    //                Debug.Log($"Destination: {currentDestination.gameObject}");
    //                Debug.Log($"Scenic: {scenicRoute.gameObject}");
    //                scenicRoute.gameObject.SetActive(true);
    //                scenicRoute.gameObject.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.blue);
    //                break;
    //            }
    //        case 2:
    //            {
    //                //Speed
    //                break;
    //            }
    //        case 3:
    //            {
    //                //Clean
    //                break;
    //            }
    //        case 4:
    //            {
    //                //Dirty
    //                break;
    //            }
    //    }

    //}
   
}
