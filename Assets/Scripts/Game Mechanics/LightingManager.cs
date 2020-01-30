using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // run while in editor to test for game
public class LightingManager : MonoBehaviour
{
    public float TimeOfDay;
    public bool manual = false;
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;
    [SerializeField, Range(0, 24)] private float timeOfDay;

    private void Update()
    {
        if(preset == null)
        {
            return;
        }
        if(Application.isPlaying)
        {
            timeOfDay += Time.deltaTime;
            timeOfDay %= 24; // makes time of day percent value as proportion of day

            if(manual)
            {
                UpdateLighting(TimeOfDay/24f);
            }
            if(!manual)
            {
                UpdateLighting(timeOfDay / 24f);
            }   
        }
        else
        {
            UpdateLighting(timeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColour.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColour.Evaluate(timePercent);

        if (directionalLight != null)
        {
            directionalLight.color = preset.directionalColour.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, -170, 0)); 
        }
    }

    private void OnValidate()
    {
        if (directionalLight != null)
        {
            return;
        }
        if(RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}
