using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonInWorldManager : MonoBehaviourPunCallbacks
{
    // Singleton pattern https://wiki.unity3d.com/index.php/Singleton
    // Reason - Need to acess PlayerUIManager script but that script is only executed when the VR Player is spawned.
    // Globally accessible - No need to search for or maintain a reference to the class.
    public static PhotonInWorldManager Instance;

    private void Awake()
    {
        //Debug.Log("VWM - awake");
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    // End Singleton pattern

    public void LeaveRoomAndLoadHomeScene()
    {
        PhotonNetwork.LeaveRoom();
        //Debug.Log("VWM - leave room");
        // Callback triggered -  OnLeftRoom()
    }


    #region Photon Callback Methods

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New Player: " + newPlayer.NickName + " joined room. Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnLeftRoom()
    {
        // Disconnect from server - relates to voice
        PhotonNetwork.Disconnect();
        //Debug.Log("VWM - OnLeftRoom > disconnect");
        // Callback called - OnDisconnected
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel("HomeScene");
        //Debug.Log("VWM - loadlevel(HomeScene)");
    }
    #endregion
}
