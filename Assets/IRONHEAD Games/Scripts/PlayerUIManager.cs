using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public GameObject VRMenu_Gameobject;
    public GameObject GoHomeButton;

    void Start()
    {
        VRMenu_Gameobject.SetActive(false);
        
        // Calls Singleton method - access public methods from external scripts
        GoHomeButton.GetComponent<Button>().onClick.AddListener(VirtualWorldManager.Instance.LeaveRoomAndLoadHomeScene);
    }
}
