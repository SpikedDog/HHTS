using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var hector = other.gameObject.GetComponentInChildren<Hector>();
            if (hector != null)
            {
                hector.transform.parent = transform;
                hector.transform.position = transform.GetChild(0).position;
            }
        }
        //CustomerDefault customer = other.GetComponentInChildren<CustomerDefault>();
        //if (customer != null && customer.currentDestination == transform)
        //{
        //    customer.DropOffCustomer();
        //}
       
    }
}
