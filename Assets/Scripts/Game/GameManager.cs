using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] destinations;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is null.");
        }
    }

    void Start()
    {
        // Hide all destinations initially
        foreach (GameObject destination in destinations)
        {
            destination.SetActive(false);
        }
    }

    public Transform GetValidDestination(Vector3 customerPosition, float minDistance = 40f)
    {
        List<Transform> validDestinations = new List<Transform>();

        foreach (GameObject destination in destinations)
        {
            if (Vector3.Distance(customerPosition, destination.transform.position) >= minDistance)
            {
                validDestinations.Add(destination.transform);
            }
        }

        if (validDestinations.Count == 0)
        {
            Debug.LogError("No valid destinations found.");
            return null;
        }

        int index = Random.Range(0, validDestinations.Count);
        return validDestinations[index];
    }
}
