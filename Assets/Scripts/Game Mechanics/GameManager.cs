using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public bool gameFinished = false;
    public string winner = "";

    //Score Variables
    public int player1Score;
    public int player2Score;

    //Van Variables
    public int van1Skin;
    public int van2Skin;
    public selectSkin skin;

    public Texture2D[] playerVanSkins;

    public static GameManager gameManagerInstance { get; set; }

    private void Awake()
    {
        if (gameManagerInstance != null)
            Destroy(gameObject);
        else
            gameManagerInstance = this;

        DontDestroyOnLoad(this);

    }
    public void Start()
    {

        //gameObject.GetComponent<selectSkin>();
    }

    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Main Menu")
        {

            van1Skin = skin.player1Choice;
            van2Skin = skin.player2Choice;
        }


        if (!gameFinished)
        {
            if (player1Score == 10)
            {
                gameFinished = true;
                winner = "Player 1";
                SceneManager.LoadScene("Winscene");

            }

            if (player2Score == 10)
            {
                gameFinished = true;
                winner = "Player 2";
                SceneManager.LoadScene("Winscene");


            }
        }
      

       // gameFinished = true;
         
        
    }
}


