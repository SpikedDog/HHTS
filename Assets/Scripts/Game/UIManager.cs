using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; } //This is cool, basically this means the UI instance cant be set by anything else but itself but other scripts can get the values. TLDR: Read only file
    public Text timerText;
    public Text pointsText;
    public Text objectivesText;
    public float timeRemaining = 180;
    private int points = 0;
    private string objectives = "Bring customer to destination";

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
    }

    void Start()
    {
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
