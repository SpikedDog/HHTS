using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] destinations;
    public float timeRemaining;
    public Text timerText;

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
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            //UpdateTimerText();
        }
        //else
        //{
        //    EndGame(); LATER TOO
        //}
    }

    //void UpdateTimerText() LATER
    //{
    //    int minutes = Mathf.FloorToInt(timeRemaining / 60);
    //    int seconds = Mathf.FloorToInt(timeRemaining % 60);
    //    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    //}

    //void EndGame()
    //{
    //    Debug.Log("Game Over!");
        //SceneManager.LoadScene("GameOverScene"); THIS WILL BE ADDED WHEN SCENE IS MADE
    //}

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
