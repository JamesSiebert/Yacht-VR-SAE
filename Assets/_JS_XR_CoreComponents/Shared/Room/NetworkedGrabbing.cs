using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkedGrabbing : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    /*
     * Attach to XR Grab Interactable Select Entered and Select Exited events
     * Photon View needs to be observing the transform and ownership transfer: Request (gives ownership if allowed, e.g you cant take someones gun) alternative "Takeover" (gives ownership to anyone who wants it) 
     *
     * More info: https://doc.photonengine.com/en-us/pun/v1/demos-and-tutorials/package-demos/ownership-transfer
     */
    
    
    private PhotonView m_photon_view; // not standard styling reflects photon's sync scripts
    Rigidbody rb;
    public bool isBeingHeld = false;
    public bool allowKinematicStateChanges = true;

    private void Awake()
    {
        m_photon_view = GetComponent<PhotonView>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if(isBeingHeld)
        {
            if(allowKinematicStateChanges)
                rb.isKinematic = true; // stops weird gravity conflict visible on clients - Breaks air hockey though
            
            gameObject.layer = 13; // Change the layer to InHand
        }
        else
        {
            if(allowKinematicStateChanges)
                rb.isKinematic = false; // stops weird gravity conflict visible on clients
            
            gameObject.layer = 8; // Change the layer to Interactable
        }
    }

    void TransferOwnership()
    {
        m_photon_view.RequestOwnership();
    }

    // Called when object is grabbed
    // Called from OnSelect Entered Event in XR Grab Interactable component
    // When player grabs object they become the owner of the object, this allows for network replication. only owner can control networked objects
    public void OnSelectEnter()
    {
        Debug.Log("Network Grab start");
        
        //ISSUE HERE
        m_photon_view.RPC("StartNetworkedGrabbing", RpcTarget.AllBuffered); // RPC - AllBuffered calls method and applies to all players even players yet to join

        if(m_photon_view.Owner == PhotonNetwork.LocalPlayer)
        {
            Debug.Log("Object already owned by player. Transfer not required");
        }
        else
        {
            Debug.Log("Call transfer ownership");
            TransferOwnership();
        }
    }

    
    // Called when object is released
    // Called from OnSelect Exited Event in XR Grab Interactable component
    public void OnSelectExit()
    {
        Debug.Log("Network Grab end");
        
        m_photon_view.RPC("StopNetworkedGrabbing", RpcTarget.AllBuffered); //RPC Call
    }

    // Callbacks target view is this view, requesting player is the player who wants to own it
    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        // Stops transferring ownership of all objects of same type e.g 2 Bows in scene, both change ownership
        if(targetView != m_photon_view){
            return;
        }
    
        Debug.Log("OnOwnership requested for: " + targetView.name + " from: " + requestingPlayer.NickName);
        
        // Transfers ownership to new player
        m_photon_view.TransferOwnership(requestingPlayer);
    }
    
    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log("Transfer is complete. New owner: " + targetView.Owner.NickName);
    }


    // Changes isBeingHeld bool on all players - this syncs gravity on/off - also prevents other players taking ownership of object
    // Remote procedure calls (RPC) are the method calls on remote clients in the same room
    // e.g sync health, inform all other players of changes
    [PunRPC] //Execute across network
    public void StartNetworkedGrabbing() // network - disable object gravity (RB - kinematic)
    {
        isBeingHeld = true;
    }

    [PunRPC] //Execute across network
    public void StopNetworkedGrabbing() // network - enable object gravity
    {
        isBeingHeld = false;
    }  
}
