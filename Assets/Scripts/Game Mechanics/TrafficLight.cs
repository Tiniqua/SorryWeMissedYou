using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public Renderer[] lightRenderer;
    public Texture2D[] lights;
    public Light[] pointLights;
    int startColour;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeLight());
    }


    IEnumerator ChangeLight()
    {
        while (true)
        { 

            int i = 0;
            startColour = Random.Range(0, 3);

            while (i < lightRenderer.Length)
            {
                lightRenderer[i].material.mainTexture = lights[startColour];
                

                if(startColour == 0)
                {
                    pointLights[0].gameObject.SetActive(true);
                    pointLights[1].gameObject.SetActive(false);
                    pointLights[2].gameObject.SetActive(false);
                }
                if (startColour == 1)
                {
                    pointLights[0].gameObject.SetActive(false);
                    pointLights[1].gameObject.SetActive(true);
                    pointLights[2].gameObject.SetActive(false);
                }
                if (startColour == 2)
                {
                    pointLights[0].gameObject.SetActive(false);
                    pointLights[1].gameObject.SetActive(false);
                    pointLights[2].gameObject.SetActive(true);
                }

                i++;
            }

            yield return new WaitForSeconds(2f);

      
        }

    }
}
