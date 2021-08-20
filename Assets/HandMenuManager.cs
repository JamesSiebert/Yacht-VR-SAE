using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class HandMenuManager : MonoBehaviour
{
    public GameObject gameManager;
    public bool isReady = false;
    public DayNightController dayNightController;

    public GameObject dayButton;
    public GameObject nightButton;
    public GameObject engineButton;
    public GameObject musicButton;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            isReady = true;
            dayNightController = gameManager.GetComponent<DayNightController>();
            
            if (dayNightController.changeEnabled)
            {
                dayButton.SetActive(true);
                nightButton.SetActive(true);
                engineButton.SetActive(true);
                musicButton.SetActive(true);
            }
            else
            {
                dayButton.SetActive(false);
                nightButton.SetActive(false);
                engineButton.SetActive(false);
                musicButton.SetActive(false);
            }
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
