using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAndVanSelect : MonoBehaviour
{

    //House Variables
    public GameObject[] Houses;
    public GameManager gameManager;
    public GameObject player1currentHouse;
    public GameObject player2currentHouse;
    public bool player1HouseAssigned = false;
    public bool player2HouseAssigned = false;

    //Player Variables
    public Player1Controller player1Controller;
    public Player2Controller player2Controller;
    private GameObject player1;
    private GameObject player2;
    public Renderer van1;
    public Renderer van2;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManagerInstance;

        Houses = GameObject.FindGameObjectsWithTag("House");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        player1Controller = player1.GetComponent<Player1Controller>();
        player2Controller = player2.GetComponent<Player2Controller>();
        player1currentHouse = null;
        player2currentHouse = null;

        van1.material.mainTexture = gameManager.playerVanSkins[gameManager.van1Skin];
        van2.material.mainTexture = gameManager.playerVanSkins[gameManager.van2Skin];

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameFinished)
        {
            if (!player1HouseAssigned && player1Controller.holdingParcel)
            {
                int index = Random.Range(0, Houses.Length);

                player1currentHouse = Houses[index];
                player1currentHouse.gameObject.tag = "player1CurrentHouse";
                player1HouseAssigned = true;
            }
            if (!player2HouseAssigned && player2Controller.holdingParcel)
            {
                int index = Random.Range(0, Houses.Length);

                player2currentHouse = Houses[index];
                player2currentHouse.gameObject.tag = "player2CurrentHouse";
                player2HouseAssigned = true;
            }
        }
    }
}
