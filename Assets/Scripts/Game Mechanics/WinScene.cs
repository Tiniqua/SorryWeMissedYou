using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScene : MonoBehaviour
{
    private GameManager gameManager;
    public TMP_Text winTitle;
    public Animator endAnimations;
    public TMP_Text player1Parcels;
    public TMP_Text player2Parcels;
    public GameObject player1;
    public GameObject player2;
    public Animator player1A;
    public Animator player2A;

    [SerializeField] private bool updated;

    private void Start()
    {

        gameManager = GameManager.gameManagerInstance;

        player1A.SetBool("Win", false);
        player2A.SetBool("Win", false);
        player1A.SetBool("Lose", false);
        player2A.SetBool("Lose", false);

        updated = false;
      
    }

    void Update()
    {
        if (gameManager.winner == "Player 1" && !updated)
        {
            winTitle.text = gameManager.winner + "Wins!";

            player1A.SetBool("Win", true);
            player2A.SetBool("Lose", true);
            updated = true;
        }
        if (gameManager.winner == "Player 2" && !updated)
        {
            winTitle.text = gameManager.winner + "Wins!";
            player1A.SetBool("Lose", true);
            player2A.SetBool("Win", true);
            updated = true;
        }
        if (gameManager.winner == "Neither of you" && !updated)
        {
            winTitle.text = gameManager.winner + "Win...";
            player1A.SetBool("Lose", true);
            player2A.SetBool("Lose", true);
            updated = true;
        }

        player1Parcels.text = "Player 1 Total Deliveries - " + gameManager.player1Score;
        player2Parcels.text = "Player 2 Total Deliveries - " + gameManager.player2Score;

    }

}
