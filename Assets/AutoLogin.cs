using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLogin : MonoBehaviour
{
    public bool enableAutoLogin;
    public LoginManager loginManager;
    public GameObject manualConnectionPanel;
    public GameObject autoConnectionPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        if (enableAutoLogin)
        {
            StartCoroutine(ExampleCoroutine());
        }
    }
    
    
    IEnumerator ExampleCoroutine()
    {
        manualConnectionPanel.SetActive(false);
        autoConnectionPanel.SetActive(true);
        
        Debug.Log("Connecting...");
        
        yield return new WaitForSeconds(3);

        Debug.Log("Connect called");
        loginManager.ConnectToPhotonServer();
    }
}
