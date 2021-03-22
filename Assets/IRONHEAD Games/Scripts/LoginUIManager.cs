using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{

    public GameObject ConnectOptionsPanelGameobject;
    public GameObject ConnectWithNamePanelGameobject;

    void Start()
    {
        ConnectOptionsPanelGameobject.SetActive(true);
        ConnectWithNamePanelGameobject.SetActive(false);
    }


    void Update()
    {
        
    }
}
