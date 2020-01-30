using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SwitchSkinMenu : MonoBehaviour
{
    public GameObject OnPanel;
    public GameObject OffPanel;
    public GameObject FirstObject;

    public GameObject[] crosses;
    
    public void SwitchToOptions()
    {
        foreach (GameObject cross in crosses)
        {
            
           cross.SetActive(false);
          
        }
       
        OffPanel.SetActive(true);
        OnPanel.SetActive(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(FirstObject, null);
    }
}
