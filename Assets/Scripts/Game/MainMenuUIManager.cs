using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class MainMenuUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnPlayButton()
    {
        SceneManager.LoadScene("MainGameLevel");
    }

    // Update is called once per frame
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
