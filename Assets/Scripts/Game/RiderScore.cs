using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiderScore : MonoBehaviour
{
    public int maxPoints;
    public int points;
    public bool timerStart = false;
    private float ticker;
    private float fractorial = 0.5f;
    public UIManager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetPoints();
    }

    private void ResetPoints()
    {
        points = maxPoints;
        ticker = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //UIManager.instance.AddPoints(points);
        if (timerStart == false)
        {
            return;
        }
        if (Time.time > ticker + fractorial)
        {
            points -= 1;
            manager.UpdateRideScore(points);
            ticker = Time.time;
        }
    }

    public void StartTimer()
    {
        timerStart = true;
        ResetPoints();
    }
}
