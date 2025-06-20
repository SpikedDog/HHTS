using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        CustomerDefault customer = other.GetComponentInChildren<CustomerDefault>();
        if (customer != null && customer.currentDestination == transform)
        {
            customer.DropOffCustomer();
        }
       
    }
}
