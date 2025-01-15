using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private SphereCollider range;
    private bool PickUpCustomer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            GameObject destination = GameManager.instance.GetDestinationNotInside(transform.position, range.radius);
            Debug.Log(destination.name);
        }
    }
}
