using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VRSpawnManager : MonoBehaviour
{

    [SerializeField]
    GameObject vrPlayerPrefab;

    // public Transform playerSpawnPosition;
    // public Vector3 spawnPosition;
    // public GameObject spawnParent;
    public Transform spawnTransform;

    void Start()
    {
        // If object provided get vector 3 from that, else use coordinates.
        // if (playerSpawnPosition != null)
        //     spawnPosition = playerSpawnPosition.position;
        
        if(PhotonNetwork.IsConnectedAndReady)
        {
            // Instantiates a player across the network for all clients
            GameObject player = PhotonNetwork.Instantiate(vrPlayerPrefab.name, spawnTransform.position, spawnTransform.rotation);
            
            // // If parent assigned make player child of that object (e.g moving boat)
            // if (spawnParent != null)
            // {
            //     player.transform.parent = spawnParent.transform;
            //     player.transform.localPosition = playerSpawnPosition.localPosition;
            // }
        }
    }
}