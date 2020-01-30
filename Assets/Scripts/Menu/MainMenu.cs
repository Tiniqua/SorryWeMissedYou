using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
 
    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame");

        //If you've gone from the pause menu to menu and back to game it wasn't resuming
        Time.timeScale = 1f;
        PauseMenu.gameIsPaused = false;

    }
    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
