using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelCollider : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.gameManagerInstance;
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "active") //if parcel hits hiding place
        {
            Destroy(gameObject); // destroy package
            gameManager.player1Score += 2; // increment score
        }
        else if (other.tag == "player1CurrentHouse")
        {
            gameManager.player1Score += 1; // increment score
        }
        else
        {
            Destroy(gameObject, 15f); // destroy after 10 seconds anyway
        }
    }
}
