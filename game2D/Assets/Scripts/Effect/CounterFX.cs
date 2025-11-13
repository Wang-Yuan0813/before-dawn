using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class CounterFX : MonoBehaviour
{
    private Light2D lightSetting;
    // Start is called before the first frame update
    private void Awake()
    {
        lightSetting = GetComponent<Light2D>();
    }
    // Update is called once per frame
    void Update()
    {
        lightSetting.intensity -= 0.08f;
        if (lightSetting.intensity <= 0)
            lightSetting.intensity = 0;
    }
}
