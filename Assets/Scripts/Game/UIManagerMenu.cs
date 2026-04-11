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

    void Awake()
    {
        //if (UIManager.instance == null)
        //{
        //    Debug.LogError("UIManager instance is null.");
        //}
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
        InGameUI = GameObject.Find("InGameUI");
        Cursor.visible = enabled;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
