using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VRSpawnManager : MonoBehaviour
{

    [SerializeField]
    GameObject vrPlayerPrefab;

    public Transform playerSpawnPosition;
    public Vector3 SpawnPosition;

    void Start()
    {
        if (playerSpawnPosition != null)
            SpawnPosition = playerSpawnPosition.position;
        
        if(PhotonNetwork.IsConnectedAndReady)
        {
            // Instantiates a player across the network for all clients
            PhotonNetwork.Instantiate(vrPlayerPrefab.name, SpawnPosition, Quaternion.identity);
        }
    }
}