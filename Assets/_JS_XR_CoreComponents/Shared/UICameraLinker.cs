using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraLinker : MonoBehaviour
{
    
    //private GameObject UICanvas;

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void Update()
    {
        // Link camera (Spawned Generic VR Player) to UI canvas
        if (GetComponent<Canvas>().worldCamera == null)
        {
            GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }
}
