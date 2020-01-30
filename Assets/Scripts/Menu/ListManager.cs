using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListManager : MonoBehaviour
{
    public TMP_Text Deliveries1;
    public TMP_Text Deliveries2;
    public TMP_Text Current1;
    public TMP_Text Current2;
    public TMP_Text Found1;
    public TMP_Text Found2;
    private GameManager gameManager;
    public HouseAndVanSelect house;
    public Player1Controller player1;
    public Player2Controller player2;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManagerInstance;
    }

    // Update is called once per frame
    void Update()
    {
      
        Deliveries1.text = "Deliveries - " + gameManager.player1Score + "/10";
        Deliveries2.text = "Deliveries - " + gameManager.player2Score + "/10";

        if (house.player1currentHouse != null)
        {
            Current1.text = "Current House - " + house.player1currentHouse.name;
        }
        else
        {
            Current1.text = "Current House - None";
        }

        if (house.player2currentHouse != null)
        {
            Current2.text = "Current House - " + house.player2currentHouse.name;
        }
        else
        {
            Current2.text = "Current House - None ";
        }




        if (player1.houseFound == true)
        {
            Found1.text = "FOUND HOUSE";
        }
        if (player1.houseFound == false && house.player1HouseAssigned == false)
        {
            Found1.text = "Grab a package";
        }
        if (player1.houseFound == false || house.player1HouseAssigned == false)
        {
            Found1.text = "Follow the arrow";
        }
        if (player2.houseFound == true)
        {
            Found2.text = "FOUND HOUSE";
        }
        if (player2.houseFound == false && house.player2HouseAssigned == false)
        {
            Found2.text = "Grab a package";
        }
        if (player2.houseFound == false || house.player2HouseAssigned == false)
        {
            Found2.text = "Follow the arrow";
        }

    }
}
