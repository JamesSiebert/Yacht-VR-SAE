using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TextMeshProUGUI OccupancyRateText_ForSchool;

    [SerializeField]
    TextMeshProUGUI OccupancyRateText_ForOutdoor;
    string mapType;

    void Start()
    {
        Debug.Log("RoomManager - start");
        
        // sync scene changes to all players.
        PhotonNetwork.AutomaticallySyncScene = true;

        if(!PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("photon NOT connected and ready");
            // Handles reconnect from disconnect
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("photon Connected and ready");
            // Join lobby again
            PhotonNetwork.JoinLobby();
            // Callback - OnConnectToMaster
        }
    }

    #region UI Callback Methods

    public void JoinRandomRoom(){
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnEnterRoomButtonClicked_Outdoor()
    {
        mapType = MultiplayerVRConstants.MAPT_TYPE_OUTDOOR;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.MAP_TYPE_KEY, mapType} };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
    }

    public void OnEnterRoomButtonClicked_School()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_SCHOOL;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.MAP_TYPE_KEY, mapType} };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
    }




    #endregion

    #region Photon Callback Methods

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to servers again");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Local player: " + PhotonNetwork.NickName + " joined: " + PhotonNetwork.CurrentRoom.Name + ". Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        if(PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))
        {
            object mapType;
            if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType))
            {
                Debug.Log("Joined room with map: " + (string)mapType);
                if((string)mapType == MultiplayerVRConstants.MAP_TYPE_SCHOOL)
                {
                    // Load school scene
                    PhotonNetwork.LoadLevel("World_School");

                } 
                else if ((string)mapType == MultiplayerVRConstants.MAPT_TYPE_OUTDOOR)
                {
                    // Load outdoor scene
                    PhotonNetwork.LoadLevel("World_Outdoor");
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New Player: " + newPlayer.NickName + " joined room. Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room List Update called");

        // Called if create, join, change property of room
        if(roomList.Count == 0 )
        {
            // No Room
            OccupancyRateText_ForSchool.text = 0 + " / " + 20;
            OccupancyRateText_ForOutdoor.text = 0 + " / " + 20;
        }

        foreach(RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            if(room.Name.Contains(MultiplayerVRConstants.MAPT_TYPE_OUTDOOR))
            {
                // Update the outdoor room map
                Debug.Log("Room is an OUTDOOR map. Player count is: " + room.PlayerCount);
                OccupancyRateText_ForOutdoor.text = room.PlayerCount + " / " + 20;
            }
            else if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_SCHOOL))
            {
                // Update the school room map
                Debug.Log("Room is a SCHOOL map. Player count is: " + room.PlayerCount);
                OccupancyRateText_ForSchool.text = room.PlayerCount + " / " + 20;
            }
            else{
                Debug.Log(room.Name + " didnt match outdoor or school");
            }
        }

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    #endregion

    #region Private Methods
    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_" + mapType + Random.Range(0,10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = {MultiplayerVRConstants.MAP_TYPE_KEY};
        // There are 2 maps, "ourdoor" and "school"

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.MAP_TYPE_KEY, mapType}};

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
    #endregion

  



}
