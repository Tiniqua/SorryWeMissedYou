using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class selectSkin : MonoBehaviour
{
    public Sprite[] vanImages;
    public Image player1Screen;
    public Image player2Screen;
    public int player1Choice;
    public int player2Choice;
    public TMP_Text player1;
    public TMP_Text player2;
    public GameManager gameManager;
    private void Start()
    {
        player1Choice = 0;
        player2Choice = 0;
        player1Screen.sprite = vanImages[player1Choice];
        player2Screen.sprite = vanImages[player2Choice];
        gameManager = GameManager.gameManagerInstance;
    }

    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Main Menu")
        {
            if (player1Choice > vanImages.Length)
            {
                player1Choice = 0;
            }
            if (player1Choice < 0)
            {
                player1Choice = vanImages.Length;
            }
            if (player2Choice > vanImages.Length)
            {
                player2Choice = 0;
            }
            if (player2Choice < 0)
            {
                player2Choice = vanImages.Length;
            }

            player1Screen.sprite = vanImages[player1Choice];
            player2Screen.sprite = vanImages[player2Choice];

            player1.text = "Van 1 Skin - " + player1Choice;
            player2.text = "Van 2 Skin - " + player2Choice;
        }

    }

    public void increasePlayer1()
    {
        player1Choice += 1;
    }

    public void decreasePlayer1()
    {
        player1Choice -= 1;
    }
    public void increasePlayer2()
    {
        player2Choice += 1;
    }

    public void decreasePlayer2()
    {
        player2Choice -= 1;
    }

}
