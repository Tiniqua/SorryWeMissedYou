using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeIn : MonoBehaviour
{
    public Image fade;
    public TMP_Text text;
    private void Start()
    {
        fade.canvasRenderer.SetAlpha(1f);
        text.canvasRenderer.SetAlpha(1f);

        fadeIn();
    }
    private void fadeIn()
    {
        fade.CrossFadeAlpha(0, 10, false);
        text.CrossFadeAlpha(0, 10, false);

    }
}
