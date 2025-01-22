using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public Text timerText;
    public Text pointsText;
    public Text objectivesText;
    public float timeRemaining = 180;
    private int points = 0;
    private string objectives = "Bring customer to destination";

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
        UpdateUI();
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateUI();
        }
        else
        {
            EndGame();
        }
    }

    void UpdateUI()
    {
        // Update Timer
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Update Points
        pointsText.text = "Points: " + points.ToString();

        // Update Objectives
        objectivesText.text = "Objectives: " + objectives;
    }

    public void AddPoints(int amount)
    {
        points += amount;
        UpdateUI();
    }

    public void SetObjectives(string newObjectives)
    {
        objectives = newObjectives;
        UpdateUI();
    }

    void EndGame()
    {
        Debug.Log("Game Over!");
        // SceneManager.LoadScene("GameOverScene"); DO THIS LATER
    }
}
