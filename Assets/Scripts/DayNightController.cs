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
    // public float fillLightIntensityDay;
    // public float fillLightTempDay;
    // public float fillLightIntensityNight;
    // public float fillLightTempNight;
    
    // Animators
    public bool jetskiDown = false;
    public Animator jetskiDoor;
    public Animator jetskiMove;
    
    public bool tableDown = false;
    public Animator tabletop;
    public Animator tableLeg1;
    public Animator tableLeg2;

    public AudioSource audioTableDown;
    public AudioSource audioTableUp;
    public AudioSource audioJetskiDown;
    public AudioSource audioJetskiUp;


    //variable where we will store the HD lighting data
    private HDAdditionalLightData sunLightData;
    // private HDAdditionalLightData fillLightData;

    public bool changeEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        if (changeEnabled)
        {
            sunLightData = sun.GetComponent<HDAdditionalLightData>();
            // fillLightData = fillLight.GetComponent<HDAdditionalLightData>();
                    
            EnableDay();
        }
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
        // fillLightData.intensity = fillLightIntensityDay;
        // fillLight.colorTemperature = fillLightTempDay;
    }

    public void EnableNightLighting()
    {
        // Sun
        sunLightData.intensity = sunIntensityNight;
        sun.GetComponent<Light>().colorTemperature = sunTempNight;
        sun.GetComponent<Light>().color = sunFilterNight;
        sun.transform.rotation = sunAngleNight.transform.rotation;

        // Fill light
        // fillLightData.intensity = fillLightIntensityNight;
        // fillLight.colorTemperature = fillLightTempNight;

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


    // For VR Toggle
    public void JetskiToggle()
    {
        if (jetskiDown)
            JetskiUp();
        else
            JetskiDown();
    }
    
    public void TableToggle()
    {
        if (tableDown)
            TableUp();
        else
            TableDown();
    }


    // Animation - open door and move jetski down
    public void JetskiDown()
    {
        jetskiDoor.SetBool("jetskiDown", true);
        jetskiMove.SetBool("jetskiDown", true);
        audioJetskiDown.PlayOneShot(audioJetskiDown.clip);
        jetskiDown = true;
    }
    
    public void JetskiUp()
    {
        jetskiDoor.SetBool("jetskiDown", false);
        jetskiMove.SetBool("jetskiDown", false);
        audioJetskiUp.PlayOneShot(audioJetskiUp.clip);
        jetskiDown = false;
    }
    
    // Animation - open door and move jetski down
    public void TableDown()
    {
        tabletop.SetBool("tableDown", true);
        tableLeg1.SetBool("tableDown", true);
        tableLeg2.SetBool("tableDown", true);
        audioTableDown.PlayOneShot(audioTableDown.clip);
        tableDown = true;
    }
    
    public void TableUp()
    {
        tabletop.SetBool("tableDown", false);
        tableLeg1.SetBool("tableDown", false);
        tableLeg2.SetBool("tableDown", false);
        audioTableUp.PlayOneShot(audioTableUp.clip);
        tableDown = false;
    }
}

    
