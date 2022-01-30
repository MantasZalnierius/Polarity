using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGames()
    {
        SceneManager.LoadScene("Movement");
    }

    public void quitGames()
    {
        Application.Quit();
    }
}
