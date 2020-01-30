using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    public Transform lookAtTransform;
    public HouseAndVanSelect houseSelect;
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (houseSelect.player1currentHouse != null)
        {
            lookAtTransform = houseSelect.player1currentHouse.transform;


            Vector3 direction = lookAtTransform.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        

    }
}
