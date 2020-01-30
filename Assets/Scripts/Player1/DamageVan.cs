using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVan : MonoBehaviour
{
    public Player1VanController player1VanController;
    public Player2VanController player2VanController;
    // Start is called before the first frame update
    public float startTime;
    void Start()
    {
         
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Van2Front" && this.tag == "Player1") // if P1 at front of Van2, Van2 not dmged & B is pressed
        {
            Debug.Log("COOL");

            if (!player2VanController.isDamaged && (Input.GetKeyDown("joystick 1 button 1")))
            {
                Debug.Log("Tamper2");
                player2VanController.isDamaged = true;
            }           
        }

        if (other.tag == "Van1Front" && this.tag == "Player2") // if P1 at front of Van2, Van2 not dmged & B is pressed
        {
            if (!player1VanController.isDamaged && (Input.GetKeyDown("joystick 2 button 1")))
            {
                Debug.Log("Tamper1");
                player1VanController.isDamaged = true;
            }
        }

        if (other.tag == "Van1Front" && this.tag == "Player1" && player1VanController.playerFixing == false) 
        {
            if (player1VanController.isDamaged && (Input.GetKeyDown("joystick 1 button 1")))
            {
                Debug.Log("Repair1");
                player1VanController.playerFixing = true;
                startTime = Time.time;
                player1VanController.barForeground.localScale = new Vector3(0, player1VanController.barForeground.localScale.y, player1VanController.barForeground.localScale.z);
            }
        }

        if (other.tag == "Van2Front" && this.tag == "Player2" && player2VanController.playerFixing == false)
        {
            if (player2VanController.isDamaged && (Input.GetKeyDown("joystick 2 button 1")))
            {
                //Debug.Log("Repair2");
                player2VanController.playerFixing = true;
                //player2VanController.repairVan();
            }
        }
    }
}
