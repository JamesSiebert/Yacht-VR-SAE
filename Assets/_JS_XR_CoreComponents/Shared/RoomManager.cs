using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TextMeshProUGUI OccupancyRateText_ForTraining;

    [SerializeField]
    TextMeshProUGUI OccupancyRateText_ForPershingIsland;

    [SerializeField]
    TextMeshProUGUI OccupancyRateText_ForPershingOcean;
    
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

    public void OnEnterRoomButtonClicked_Training()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_TRAINING;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.MAP_TYPE_KEY, mapType} };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
    }

    public void OnEnterRoomButtonClicked_PershingIsland()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_PERSHING_ISLAND;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.MAP_TYPE_KEY, mapType} };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
    }

    public void OnEnterRoomButtonClicked_PershingOcean()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_PERSHING_OCEAN;
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
                
                if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_TRAINING)
                {
                    // Load scene
                    PhotonNetwork.LoadLevel("Room_Training");
                }
                else if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_PERSHING_ISLAND)
                {
                    // Load scene
                    PhotonNetwork.LoadLevel("Room_PershingIsland");
                }
                else if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_PERSHING_OCEAN)
                {
                    // Load scene
                    PhotonNetwork.LoadLevel("Room_PershingOcean");
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
            OccupancyRateText_ForTraining.text = 0 + " / " + 20;
            OccupancyRateText_ForPershingIsland.text = 0 + " / " + 20;
            OccupancyRateText_ForPershingOcean.text = 0 + " / " + 20;
        }

        foreach(RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            
            if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_TRAINING))
            {
                Debug.Log("Room is a TRAINING map. Player count is: " + room.PlayerCount);
                OccupancyRateText_ForTraining.text = room.PlayerCount + " / " + 20;
            }
            else if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_PERSHING_ISLAND))
            {
                Debug.Log("Room is a PERSHING ISLAND map. Player count is: " + room.PlayerCount);
                OccupancyRateText_ForPershingIsland.text = room.PlayerCount + " / " + 20;
            }
            else if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_PERSHING_OCEAN))
            {
                Debug.Log("Room is a PERSHING OCEAN map. Player count is: " + room.PlayerCount);
                OccupancyRateText_ForPershingOcean.text = room.PlayerCount + " / " + 20;
            }
            else
            {
                Debug.Log(room.Name + " didnt match map names");
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

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.MAP_TYPE_KEY, mapType}};

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
    #endregion
}
