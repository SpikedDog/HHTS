using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDefault : MonoBehaviour
{
    //public int points;
    public Transform currentDestination;
    //public Transform scenicRoute;
    [HideInInspector]public bool isPickedUp = false;
    private ArrowPointer arrowController;
    //public Renderer sphereRenderer;
    //public GameObject Hector;
    private RiderScore rideScore;
    private CustomerManager customerManager;
    private UIManager uiManager;

    void Start()
    {
        arrowController = FindObjectOfType<ArrowPointer>();
        rideScore = FindObjectOfType<RiderScore>();
        customerManager = FindObjectOfType<CustomerManager>();
        uiManager = FindObjectOfType<UIManager>();
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
        customerManager.TurnOffInteract();
        int index = customerManager.FindIndex(this);
        customerManager.RemoveInteract(index);
        
        rideScore.StartTimer(); //RS STARTS COUNT ON PICKUP
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            //transform.SetParent(player.transform);
            //transform.localPosition = Vector3.zero;
            
            //gameObject.GetComponent<MeshRenderer>().enabled = false; //CHANGE WHEN ANIMATION IS MADE!!
            currentDestination.gameObject.SetActive(true);
            UIManager.instance.SetObjectives("Take customer to the destination: " + currentDestination.name);
            arrowController.SetDestination(currentDestination); // Sets the destination on the arrow
            //if (sphereRenderer != null)
            //{
            //    sphereRenderer.enabled = false;
            //}
            transform.position = new Vector3(0,-2000,0);//Hide
            transform.parent = player.transform;
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
            UIManager.instance.ClearObjectives();
            arrowController.ClearDestination(); // Clear the destination on the arrow
            //if (sphereRenderer != null) //Customer Sphere disappears when picked up
            //{
            //    sphereRenderer.enabled = true;
            //}
            if (currentDestination != null)
            {
                currentDestination.gameObject.SetActive(false);
            }
            rideScore.StopTimer();  //RS ADD HERE
            customerManager.TurnOnInteract();
            Destroy(gameObject, 7.5f);
            customerManager.RemoveHector(this);
            customerManager.TurnOnHector();
            Debug.Log("TesterDefault");
        }
    }

    public void Interact()
    {
        if (isPickedUp == false)
        {
            PickUpCustomer();
        }
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
