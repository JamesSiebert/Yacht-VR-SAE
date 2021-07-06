using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantVariables : MonoBehaviour
{
    public bool tutorialPlayed = false;

    private void Awake()
    {

        // hyper-advanced singleton implementation
        if (FindObjectsOfType(typeof(PersistantVariables)).Length > 1)
        {
            Debug.Log("Already found instance of script in scene; destroying.");
            DestroyImmediate(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
