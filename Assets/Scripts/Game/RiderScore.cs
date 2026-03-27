using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiderScore : MonoBehaviour
{
    public int setPoints = 100;
    public int maxPoints;
    public int rsPoints;
    public bool timerStart = false;
    private float ticker;
    private float fractorial = 0.5f;
    public UIManager manager;

    // Start is called before the first frame update
    void Start()
    {
        RoundPoints();
    }

    private void ResetPoints()
    {
        rsPoints = setPoints;
        manager.UpdateRideScore(rsPoints);
    }

    private void RoundPoints()
    {
        rsPoints = maxPoints;
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
            rsPoints -= 1;
            manager.UpdateRideScore(rsPoints);
            ticker = Time.time;
        }
    }

    public void StartTimer()
    {
        timerStart = true;
        RoundPoints();
    }

    public void StopTimer()
    {
        manager.AddPoints(rsPoints);
        timerStart = false;
        ResetPoints();
        Debug.Log("Calling Reset Points");
    }
}
