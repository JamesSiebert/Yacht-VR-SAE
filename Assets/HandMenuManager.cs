using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuManager : MonoBehaviour
{

    public GameObject gameManager;
    public bool isReady = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            isReady = true;
        }
    }
    

    public void ActivateDayLighting()
    {
        if (isReady)
        {
            gameManager.GetComponent<DayNightController>().EnableDayLighting();
        }
        
    }
    
    public void ActivateNightLighting()
    {
        if (isReady)
        {
            gameManager.GetComponent<DayNightController>().EnableNightLighting();
        }
        
    }
    
}
