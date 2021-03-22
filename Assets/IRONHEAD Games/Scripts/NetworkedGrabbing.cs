using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkedGrabbing : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{

    PhotonView m_photon_view;
    Rigidbody rb;
    public bool isBeingHeld = false;

    private void Awake()
    {
        m_photon_view = GetComponent<PhotonView>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isBeingHeld)
        {
            rb.isKinematic = true;
            gameObject.layer = 13; // Change the layer to InHand
        }
        else
        {
            rb.isKinematic = false;
            gameObject.layer = 8; // Change the layer to Interactable
        }
    }

    void TransferOwnership()
    {
        m_photon_view.RequestOwnership();
    }

    // when player grabs object it becomes the owner of the object, this allows for network replication
    public void OnSelectEnter()
    {
        Debug.Log("Network Grab start");
        m_photon_view.RPC("StartNetworkedGrabbing", RpcTarget.AllBuffered); // calls method and applies to all players even players yet to join

        if(m_photon_view.Owner == PhotonNetwork.LocalPlayer)
        {
            Debug.Log("Object already owned by player. Transfer not required");
        }
        else
        {
            TransferOwnership();
        }
    }

    public void OnSelectExit()
    {
        Debug.Log("Network Grab end");
        m_photon_view.RPC("StopNetworkedGrabbing", RpcTarget.AllBuffered);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        // Stops transferring ownership of all objects of same type
        if(targetView != m_photon_view){
            return;
        }

        Debug.Log("OnOwnership requested for: " + targetView.name + " from: " + requestingPlayer.NickName);
        m_photon_view.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log("Transfer is complete. New owner: " + targetView.Owner.NickName);
    }


    // Remote procedure calls (RPC) are the method calls on remote clients in the same room
    // e.g sync health, inform all other players of changes
    [PunRPC]
    public void StartNetworkedGrabbing() // network - disable object gravity (RB - kinematic)
    {
        isBeingHeld = true;
    }

    [PunRPC]
    public void StopNetworkedGrabbing() // network - enable object gravity
    {
        isBeingHeld = false;
    }  
}
