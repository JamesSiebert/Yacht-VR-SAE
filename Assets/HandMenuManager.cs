using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuManager : MonoBehaviour
{

    public GameObject gameManager;
    public bool isReady = false;
    public DayNightController dayNightController;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            isReady = true;
            dayNightController = gameManager.GetComponent<DayNightController>();
        }
    }
    

    public void ActivateDay()
    {
        if (isReady)
            dayNightController.EnableDay();
    }
    
    public void ActivateNight()
    {
        if (isReady)
            dayNightController.EnableNight();
    }
    
    public void ToggleEngineSounds()
    {
        if (isReady)
            dayNightController.ToggleEngineSounds();
    }
    
    public void ToggleStereoSounds()
    {
        if (isReady)
            dayNightController.ToggleStereoSounds();
    }
    
}
