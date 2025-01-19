using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject customer;
    public Transform[] spawnPoints;
    public int maxSpawnCount = 10;
    //public float spawnRadius = 25f;
    //public float minDisatance = 10f;

    private List<GameObject> spawnedCustomers = new List<GameObject> ();

    // Start is called before the first frame update
    void Start()
    {
        SpawnCustomers();
    }

    void SpawnCustomers()
    {
        int spawnCount = Mathf.Min(maxSpawnCount, spawnPoints.Length);
        for (int i = 0; i < spawnCount; i++)
        {
            Transform spawnPoint = spawnPoints[i];
            GameObject spawnedPrefab = Instantiate(customer, spawnPoint.position, spawnPoint.rotation);
            spawnedCustomers.Add(spawnedPrefab);
        }
    }

    //void SpawnCustomers()
    //{
    //    for (int i = 0; i < maxSpawnCount; i++)
    //    {
    //        Vector3 spawnPos;
    //        int attempts = 0;
    //        do
    //        {
    //            spawnPos = GetRandomPos();
    //            attempts++;
    //        } while (!IsPositionValid(spawnPos) && attempts < 100);

    //        if (attempts < 100)
    //        {
    //            GameObject spawnedCustomer = Instantiate(customer, spawnPos, Quaternion.identity);
    //            spawnedCustomers.Add(spawnedCustomer);
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Could not find a valid pos for customer after 100 attempts");
    //        }
    //    }
    //}

    //Vector3 GetRandomPos()
    //{
    //    Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
    //    randomDirection += transform.position;
    //    randomDirection.y = 2.968937f;
    //    return randomDirection;
    //}

    //bool IsPositionValid(Vector3 pos)
    //{
    //    foreach (GameObject spawnedCustomer in spawnedCustomers)
    //    {
    //        if (Vector3.Distance(pos, spawnedCustomer.transform.position) < minDisatance)
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    //private void OnTriggerEnter(Collider player)
    //{
    //    if (player.CompareTag("Player"))
    //    {
    //        GameObject destination = GameManager.instance.GetDestinationNotInside(transform.position, range.radius);
    //        Debug.Log(destination.name);
    //    }
    //}
}
