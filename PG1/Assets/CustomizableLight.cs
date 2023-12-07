using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableLight : MonoBehaviour
{
    public Color lightColor = Color.white;
    public bool flickering = false;
    public bool pulsing = false;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 1f;
    public float flickerProbability = 0.1f;

    private Light lightComponent;

    void Start()
    {
        lightComponent = GetComponent<Light>();
    }

    void Update()
    {
        lightComponent.color = lightColor;

        if (pulsing){
            float flickerValue = Mathf.PingPong(Time.time * flickerSpeed, 1.0f);
            lightComponent.intensity = Mathf.Lerp(minIntensity, maxIntensity, flickerValue);
        }

        if (flickering && Random.value < flickerProbability)
        {
            lightComponent.intensity = (lightComponent.intensity == minIntensity) ? maxIntensity : minIntensity;
        }
    }
}
