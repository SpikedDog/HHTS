using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour
{
    // Main Menu Toggles
    public void OnPlayButton()
    {
        SceneManager.LoadScene("MainGameLevel");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
