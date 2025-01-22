using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] destinations;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //UpdateTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public GameObject GetDestinationNotInside(Vector3 position, float radius)
    //{
    //    int count = 10;
    //    GameObject random = destination[Random.Range(0, destination.Length)];
    //    while (count > 0 && Vector3.Distance(random.transform.position, position) < radius)
    //    {
    //        Debug.Log($"Distance from range is {random.transform.position}");
    //        random = destination[Random.Range(0, destination.Length)];
    //        count--;
    //    }
    //    return random;
    //}



    //[System.Serializable]
    //public class Destinations
    //{
    //    public GameObject[] destination;
    //}
}
