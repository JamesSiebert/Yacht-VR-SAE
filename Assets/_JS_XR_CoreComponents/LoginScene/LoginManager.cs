using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerName_InputField;

    #region UNITY Methods


    void Start()
    {

    }

    void Update()
    {
        
    }

    #endregion
    


    #region UI Callback Methods
    public void ConnectToPhotonServer(){
        Debug.Log("Connect to photon server called");

        if(PlayerName_InputField != null){
            PhotonNetwork.NickName = PlayerName_InputField.text;
            PhotonNetwork.ConnectUsingSettings();
        }

        else
        {
            PhotonNetwork.NickName = "";
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }
    #endregion



    #region Photon Callback Methods

    public override void OnConnected()
    {
        Debug.Log("OnConnected is called. Server is available");
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the Master Server with player name:" + PhotonNetwork.NickName);
        PhotonNetwork.LoadLevel("HomeScene");
        
    }
    #endregion
}
