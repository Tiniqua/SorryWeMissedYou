using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] Image crossImage;
    private void Update()
    {
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        crossImage.gameObject.SetActive(true);
        
    }

    public void OnDeselect(BaseEventData eventData)
    {
        crossImage.gameObject.SetActive(false);
       
    }
}
