using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGetChildren : MonoBehaviour
{
    //public GameManager gameManager;
    private BoxCollider[] hiding;
    public List<GameObject> Hiding = new List<GameObject>();
    public Light glow;
    public GameObject house1;
    public GameObject house2;
    private void Start()
    {
        //house1 = GameObject.FindGameObjectWithTag("player1CurrentHouse");
        //house1 = GameObject.FindGameObjectWithTag("player2CurrentHouse");

        glow = GetComponent<Light>();
        hiding = gameObject.GetComponentsInChildren<BoxCollider>(); // grab all children with colliders of a house region and put in array
        RefineArray();
        glow.intensity = 0;

        
    }

    private void Update()
    {
        
        //if (gameObject.CompareTag("player1CurrentHouse"))
        //{
        //    glow.intensity = 10;
        //    glow.color = Color.cyan;
        //}
        
        //if (this.gameObject.tag == "player2CurrentHouse")
        //{
        //    glow.intensity = 10;
        //    glow.color = Color.magenta;
        //}
        //else
        //{
        //    glow.intensity = 0;
        //    glow.color = Color.white;
        //}
    }

    private void RefineArray()
    {
        for (int i = 0; i < hiding.Length; i++) // for each child with collider
        {
            if ((hiding[i].tag == "HidingPlace")) // if tag is a hiding place 
            {
                hiding[i].gameObject.tag = "active"; // set gameobject tag to active
                Hiding.Add(hiding[i].gameObject); // add hiding place to list - USED TO CHECK IF PARCEL HAS HIT A HIDING PLACE 
            }
        }
    }
}
