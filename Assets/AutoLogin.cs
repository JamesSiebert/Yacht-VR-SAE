using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLogin : MonoBehaviour
{
    public LoginManager loginManager;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }
    
    
    IEnumerator ExampleCoroutine()
    {
        Debug.Log("Connecting...");
        
        yield return new WaitForSeconds(3);

        Debug.Log("Connect called");
        loginManager.ConnectToPhotonServer();
    }
}
