using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using static UIManagerMenu;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; } //This is cool, basically this means the UI instance cant be set by anything else but itself but other scripts can get the values. TLDR: Read only file*/
    public TMP_Text timerText;
    public TMP_Text pointsText;
    public TMP_Text objectivesText;
    public TMP_Text riderScore;
    public float timeRemaining = 180;
    [SerializeField] private int points = 0;
    private string objectives = "Pick up CUSTOMERS to make BUXS!";
    private GameObject InGameUI;
    private GameObject nameInput;
    public GameObject nameInputText;
    public TMP_Text totalText;
    public TMP_Text nameInputted;
    public GameObject background;
    public bool isGameOver = false;
    public bool isGameStarted = false;
    public int countdownTime;
    public int endCountdown;
    public TMP_Text countdownText;
    public GameObject menuManager;
    [SerializeField] private GameObject player;



    [Header("Leaderboard Attributes")]
    
    [SerializeField] TMP_InputField nameInputField;



    void Awake()
    {
        //if (UIManager.instance == null)
        //{
        //    Debug.LogError("UIManager instance is null.");
        //}

        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    void Start()
    {
        menuManager = UIManagerMenu.instance.gameObject;
        InGameUI = GameObject.Find("InGameUI");
        nameInput = GameObject.Find("NameInputField");
        nameInput.SetActive(false);
        nameInputText.SetActive(false);
        totalText.gameObject.SetActive(false);
        InGameUI.SetActive(false);
        background.SetActive(false);
        Cursor.visible = enabled;
        Cursor.lockState = CursorLockMode.Confined;
        player.gameObject.GetComponent<GoofyNewControls>().enabled = false;
        StartCoroutine(CountDownStart());
        UpdateUI();
    }

    void Update()
    {
        if (isGameStarted == true)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateUI();
            }
            else
            {
                EndGame();
            }
        }
    }

    void UpdateUI()
    {
        // Update Timer
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Update Points
        pointsText.text = "BUXS: " + points.ToString();

        // Update Objectives
        objectivesText.text = objectives;
        //Debug.Log("Points: " + points);
    }

    public void AddPoints(int amount)
    {
        points += amount;
        UpdateUI();
    }

    public void UpdateRideScore(int amount)
    {
        riderScore.text = $"Ride Score: {amount}";
    }

    public void SetObjectives(string newObjectives)
    {
        objectives = newObjectives;
        UpdateUI();
    }

    public void ClearObjectives()
    {
        objectives = "Pick up CUSTOMERS to make BUXS!";
        UpdateUI();
    }

    public void EndGame()
    {
        isGameOver = true;
        //Debug.Log("Game Over!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = enabled;
        if (player != null)
        {
            var controls = player.GetComponent<GoofyNewControls>();
            if (controls != null)
            {
                controls.StopMotor();
            }
        }
        player.gameObject.GetComponent<GoofyNewControls>().enabled = false;
        InGameUI.SetActive(false);
        countdownText.gameObject.SetActive(true);
        StartCoroutine(EndCounter());
        //SceneManager.LoadScene("GameOverScene");
    }

    IEnumerator CountDownStart()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        countdownText.text = "GO";
        isGameStarted = true;
        player.gameObject.GetComponent<GoofyNewControls>().enabled = true;
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
        InGameUI.SetActive(true);
    }

    IEnumerator EndCounter()
    {
        while (endCountdown > 0)
        {
            countdownText.text = "GAME OVER";
            yield return new WaitForSeconds(3f);
            endCountdown--;
        }
        countdownText.gameObject.SetActive(false);
        background.SetActive(true);
        nameInput.SetActive(true);
        nameInputText.SetActive(true);
        totalText.text = "Total BUXS: " + points.ToString();
        totalText.gameObject.SetActive(true);
    }

    public void NameEntered()
    {
        AddToLeaderboard(points, nameInputted);
    }

    public void AddToLeaderboard(int score, TMP_Text name)
    {
        if (menuManager != null)
        {
            HighScoreTable.AddHighScoreEntry(score, name.text);
            //menuManager.GetComponent<UIManagerMenu>().pointsTransfer = score;
            //Debug.Log("Score transferred: " + menuManager.GetComponent<UIManagerMenu>().pointsTransfer);
            //Debug.Log("Name entered: " + name.text);
            //menuManager.GetComponent<UIManagerMenu>().nameTransfer.text = name.text;
            //Debug.Log("Name transferred: " + menuManager.GetComponent<UIManagerMenu>().nameTransfer);
            UIManagerMenu.instance.DataTransfer();
        }
    }
}
