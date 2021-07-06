using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoNavigateToTutorial : MonoBehaviour
{
    public GameObject persistantVariablesGO;
    public PersistantVariables persistantVariables;
    public HomeRoomManager roomManager;

    public GameObject sceneSelectionCanvas;
    public GameObject autoLoadTutorialCanvas;

    void Start()
    {
        persistantVariablesGO = GameObject.Find("PersistantVariables");
        persistantVariables = persistantVariablesGO.GetComponent<PersistantVariables>();
        
        if (!persistantVariables.tutorialPlayed)
        {
            sceneSelectionCanvas.SetActive(false);
            autoLoadTutorialCanvas.SetActive(true);
            StartCoroutine(ChangeScene());
        }
        
    }

    IEnumerator ChangeScene()
    {
        Debug.Log("Navigating to tutorial" + Time.time);

        yield return new WaitForSeconds(2);

        Debug.Log("Tutorial scene change called");
        persistantVariables.tutorialPlayed = true;
        roomManager.OnEnterRoomButtonClicked_Training();
        
    }
    
}
