using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DayNightController : MonoBehaviour
{
    // Sky
    // public GameObject sky;
    // public Cubemap daySkyCubemap;
    // public Cubemap nightSkyCubemap;

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


    // Fill Light
    public Light fillLight;
    public float fillLightIntensityDay;
    public float fillLightTempDay;
    public float fillLightIntensityNight;
    public float fillLightTempNight;

    
    //variable where we will store the HD lighting data
    private HDAdditionalLightData sunLightData;
    private HDAdditionalLightData fillLightData;

    // Start is called before the first frame update
    void Start()
    {
        sunLightData = sun.GetComponent<HDAdditionalLightData>();
        fillLightData = fillLight.GetComponent<HDAdditionalLightData>();
        
        StartCoroutine(ExampleCoroutine());
        
        //EnableDayLighting();
        //EnableNightLighting();

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
        
        // // Sky cubemap
        // sky.GetComponent<PhysicallyBasedSky>().spaceEmissionTexture.value = nightSkyCubemap;
    }
    

    IEnumerator ExampleCoroutine()
    {
        EnableDayLighting();

        yield return new WaitForSeconds(10);
        
        EnableNightLighting();
    }
}
