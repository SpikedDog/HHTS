using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    [SerializeField] MyCarSound myCarSound;

    void Start()
    {
        myCarSound = FindObjectOfType<MyCarSound>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var hector = other.gameObject.GetComponentInChildren<CustomerDefault>();
            if (hector != null)
            {
                myCarSound.PlayDeliverSound();
                Debug.Log("Customer Dropped Off");
                hector.transform.parent = transform;
                hector.transform.position = transform.GetChild(0).position;
                hector.DropOffCustomer();
            }
        }
        //CustomerDefault customer = other.GetComponentInChildren<CustomerDefault>();
        //if (customer != null && customer.currentDestination == transform)
        //{
        //    customer.DropOffCustomer();
        //}
       
    }
}
