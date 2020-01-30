using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkin : MonoBehaviour
{
    GameManager gameManager;
    public Renderer renderer1;
    public Renderer renderer2;

    // Start is called before the first frame update
    void Start()
    {
        renderer1.material.mainTexture = gameManager.playerVanSkins[gameManager.van1Skin];
        renderer2.material.mainTexture = gameManager.playerVanSkins[gameManager.van2Skin];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
