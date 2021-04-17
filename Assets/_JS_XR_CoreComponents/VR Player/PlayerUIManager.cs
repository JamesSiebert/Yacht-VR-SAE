using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public GameObject handMenu;
    public GameObject homeButton;

    void Start()
    {
        handMenu.SetActive(false);
        
        // Calls Singleton method - access public methods from external scripts
        homeButton.GetComponent<Button>().onClick.AddListener(PhotonInWorldManager.Instance.LeaveRoomAndLoadHomeScene);
    }
}
