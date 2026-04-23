using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomerManager : MonoBehaviour
{
    public GameObject interactSphere;
    public GameObject hector;
    public Transform[] spawnPoints;
    public int maxSpawnCount;
    //public float spawnRadius = 25f;
    //public float minDisatance = 10f;

    private List<GameObject> spawnedInteract = new List<GameObject> ();
    private List<GameObject> spawnedHectors = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //for (i = 10)
        //{
        //    SpawnHector()
        //}
        SpawnCustomers();
        Shuffle();
        TurnOffInteract();
        TurnOffHector();
        TurnOnFirstSix();
    }

    void SpawnCustomers()
    {
        int spawnCount = Mathf.Min(maxSpawnCount, spawnPoints.Length);
        for (int i = 0; i < spawnCount; i++)
        {
            Transform spawnPoint = spawnPoints[i];
            GameObject spawnedPrefab = Instantiate(interactSphere, spawnPoint.position, spawnPoint.rotation);
            GameObject hectorPrefab = Instantiate(hector, new Vector3(spawnPoint.position.x, 1.1f,
                spawnPoint.position.z), spawnPoint.rotation);
            spawnedInteract.Add(spawnedPrefab);
            spawnedHectors.Add(hectorPrefab);
            spawnedPrefab.transform.GetChild(0).GetComponent<CustomerInteract>().customerDefault = hectorPrefab.GetComponent<CustomerDefault>();
        }
    }

    //SpawnHector()
    //{
    //    if (SpawnPosInt > 10)
    //    {
    //        SpawnPosInt = 0
    //    }
    //    Instantiate(Hector, HectorSpawnPos(SpawnPosInt));
    //}

    void Shuffle()
    {
        for (int i = 0; i < 100; i++)
        {
            int c1 = Random.Range(0, spawnedInteract.Count);
            int c2 = Random.Range(0, spawnedInteract.Count);
            GameObject temp = spawnedInteract[c1];
            spawnedInteract[c1] = spawnedInteract[c2];
            spawnedInteract[c2] = temp;

            
            GameObject tempHec = spawnedHectors[c1];
            spawnedHectors[c1] = spawnedHectors[c2];
            spawnedHectors[c2] = tempHec;
        }
    }
  

    public void RemoveInteract(int index)
    {
        spawnedInteract.RemoveAt(index);
    }

    public void RemoveHector(CustomerDefault hector) //public void RemoveHector(CustomerDefault hector)
    {
        spawnedHectors.Remove(hector.gameObject);
        //spawnedHectors.Remove(hector.gameObject);
    }

    public void TurnOffInteract()
    {
        for (int i = 0; i < spawnedInteract.Count; i++)
        {
            spawnedInteract[i].SetActive(false);
        }
    }

    public void TurnOnInteract()
    {
        for (int i = 0; i < 6; i++) //int i = 0; i < spawnedInteract.Count; i++
        {
            spawnedInteract[i].SetActive(true);
        }
    }

    public void TurnOnHector()
    {
        for (int i = 0; i < 6; i++) //int i = 0; i < 6; i++
        {
            spawnedHectors[i].SetActive(true);
        }
    }

    public void TurnOffHector()
    {
        for (int i = 0; i < spawnedInteract.Count; i++)
        {
            spawnedHectors[i].SetActive(false);
        }
    }

    public int FindIndex(CustomerDefault hector)
    {
        for (int i = 0; i < spawnedHectors.Count; i++)
        {
            if (spawnedHectors[i] == hector.gameObject)
            {
                return i;
            }
        }
        return -1;
    }

    void TurnOnFirstSix()
    {
        for (int i = 0; i < 6; i++)
        {
            spawnedInteract[i].SetActive(true);
            spawnedHectors[i].SetActive(true);
        }
    }

    public void SpawnNewHec()
    {

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
