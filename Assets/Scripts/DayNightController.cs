using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DayNightController : MonoBehaviour
{
    // Sun
    public GameObject sun;
    public Transform sunAngleDay;
    public Transform sunAngleNight;
    public float sunIntensityDay;
    public float sunTempDay;
    public float sunIntensityNight;
    public float sunTempNight;
    public Color sunFilterDay;
    public Color sunFilterNight;

    // Audio
    public GameObject[] daySounds;
    public GameObject[] nightSounds;
    public GameObject[] engineSounds;
    public bool engineSoundsActive = false;
    public GameObject[] stereoSounds;
    public bool stereoSoundsActive = false;

    // Fill Light
    public Light fillLight;
    public float fillLightIntensityDay;
    public float fillLightTempDay;
    public float fillLightIntensityNight;
    public float fillLightTempNight;


    //variable where we will store the HD lighting data
    private HDAdditionalLightData sunLightData;
    private HDAdditionalLightData fillLightData;

    public bool changeEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        sunLightData = sun.GetComponent<HDAdditionalLightData>();
        fillLightData = fillLight.GetComponent<HDAdditionalLightData>();
        
        EnableDay();
    }

    // Called by hand menu
    public void EnableDay()
    {
        EnableDayLighting();
        EnableDaySounds();
    }

    // Called by hand menu
    public void EnableNight()
    {
        EnableNightLighting();
        EnableNightSounds();
    }

    public void EnableDayLighting()
    {
        // Sun

        sunLightData.intensity = sunIntensityDay;
        sun.GetComponent<Light>().colorTemperature = sunTempDay;
        sun.GetComponent<Light>().color = sunFilterDay;
        sun.transform.rotation = sunAngleDay.transform.rotation;

        //Fill light
        fillLightData.intensity = fillLightIntensityDay;
        fillLight.colorTemperature = fillLightTempDay;
    }

    public void EnableNightLighting()
    {
        // Sun
        sunLightData.intensity = sunIntensityNight;
        sun.GetComponent<Light>().colorTemperature = sunTempNight;
        sun.GetComponent<Light>().color = sunFilterNight;
        sun.transform.rotation = sunAngleNight.transform.rotation;

        // Fill light
        fillLightData.intensity = fillLightIntensityNight;
        fillLight.colorTemperature = fillLightTempNight;

    }

    // Called from hand menu
    public void ToggleEngineSounds()
    {
        // Invert engine sounds tracker
        engineSoundsActive = !engineSoundsActive;
        
        // Trigger array items on / off
        toggleSoundArray(engineSounds, engineSoundsActive);
    }
    
    // Called from hand menu
    public void ToggleStereoSounds()
    {
        // Invert engine sounds tracker
        stereoSoundsActive = !stereoSoundsActive;
        
        // Trigger array items on / off
        toggleSoundArray(stereoSounds, stereoSoundsActive);
    }

    public void EnableDaySounds()
    {
        toggleSoundArray(nightSounds, false);
        toggleSoundArray(daySounds, true);
    }

    public void EnableNightSounds()
    {
        toggleSoundArray(daySounds, false);
        toggleSoundArray(nightSounds, true);
    }

    // Turn all items in array on or off
    public void toggleSoundArray(GameObject[] soundArray, bool active)
    {
        foreach (var sound in soundArray)
        {
            sound.SetActive(active);
        }
    }
}

    
