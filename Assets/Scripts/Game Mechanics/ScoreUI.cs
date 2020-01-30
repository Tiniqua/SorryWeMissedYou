using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text player1Score;
    public TMP_Text player2Score;
    public GameManager gameManager;

  
    // Update is called once per frame
    void Update()
    {
        player1Score.text = "Player 1 Score: " + gameManager.player1Score;
        player2Score.text = "Player 2 Score: " + gameManager.player2Score;
    }

    void LoadWinScene()
    {
        if(gameManager.gameFinished)
        {
            if(gameManager.player1Score > gameManager.player2Score)
            {
                gameManager.winner = "Player 1";
            }
            else if (gameManager.player1Score < gameManager.player2Score)
            {
                gameManager.winner = "Player 2";
            }
            else
            {
                gameManager.winner = "Neither of you"; //If its a draw
            }
        }
        SceneManager.LoadScene("WinScene");
    }
}
