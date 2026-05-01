using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class UIManagerMenu : MonoBehaviour
{
    public static UIManagerMenu instance { get; private set; } //This is cool, basically this means the UI instance cant be set by anything else but itself but other scripts can get the values. TLDR: Read only file*/
    private GameObject InGameUI;
    public Scene scene;
    public int pointsTransfer;
    public TMP_Text nameTransfer;

    void Awake()
    {
        //if (UIManager.instance == null)
        //{
        //    Debug.LogError("UIManager instance is null.");
        //}
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        scene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        if (scene.name == "MainMenuNew" || scene.name == "PostGameMenu")
        {
            InGameUI = GameObject.Find("InGameUI");
            Cursor.visible = enabled;
            Cursor.lockState = CursorLockMode.Confined;
        }

        if (scene.name == "MainMenuNew")
        {
            pointsTransfer = 0;
            nameTransfer.text = "";
        }
    }

    public void DataTransfer()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Loading Scene 2");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
