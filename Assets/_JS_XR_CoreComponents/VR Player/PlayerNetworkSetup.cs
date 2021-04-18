using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;


public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    public GameObject xrRig;
    public GameObject mainAvatar;
    public GameObject avatarHead;
    public GameObject avatarBody;
    public GameObject avatarLeftHand;
    public GameObject avatarRightHand;
    //public GameObject controllerParent;
    public GameObject[] avatarModelPrefabs;
    public TextMeshProUGUI playerNameText;
    public int localAvatarHeadLayer = 0;
    public int localAvatarBodyLayer = 0;
    public int localAvatarHandsLayer = 0;


    public XR_TeleportControlSwitcher xrTeleportControlSwitcher;

    void Start()
    {
        // Setup player
        if(photonView.IsMine)
        {
            // Local Player
            xrRig.SetActive(true); // activate local xr rig for this player only
            xrTeleportControlSwitcher.thisIsMaster = true; // enables avatar movement updates

            // Get avatar selection data so we can instantiate correct avatar model.
            object avatarSelectionNumber;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerVRConstants.AVATAR_SELECTION_NUMBER, out avatarSelectionNumber))
            {
                Debug.Log("Avatar selection number: " + (int)avatarSelectionNumber);
                photonView.RPC("InitializeSelectedAvatarModel", RpcTarget.AllBuffered, (int)avatarSelectionNumber);
            }
            
            // Hide local player head and body
            SetLayerRecursively(avatarHead, localAvatarHeadLayer);
            SetLayerRecursively(avatarBody, localAvatarBodyLayer);
            SetLayerRecursively(avatarLeftHand, localAvatarHandsLayer);
            SetLayerRecursively(avatarRightHand, localAvatarHandsLayer);

            // Finds object that have teleportation component
            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();
            if(teleportationAreas.Length > 0)
            {
                //Debug.Log("Found " + teleportationAreas.Length + " teleporation areas.");
                foreach (var item in teleportationAreas)
                {
                    // Assigns the spawned XR Rigs teleporation provider to each teleporation area
                    item.teleportationProvider = xrRig.GetComponent<TeleportationProvider>();
                }
            }

            // Add audio listener to avatar - only 1 audio listener in scene
            mainAvatar.AddComponent<AudioListener>();
        }
        else
        {
            // Remote Player
            xrRig.SetActive(false); // Deactivate all other xr rigs (only avatar will be left from other players)
            
            // Movement controllers + Avatar input converter located within XR Rig > controllerParent > XR_Teleport Control Switcher so they will also be disabled on XR rig deactivation

            // Show head and body
            SetLayerRecursively(avatarHead, 0);
            SetLayerRecursively(avatarBody, 0);
            SetLayerRecursively(avatarLeftHand, 0);
            SetLayerRecursively(avatarRightHand, 0);
        }

        if(playerNameText != null)
        {
            playerNameText.text = photonView.Owner.NickName;
        }
    }

    // Sets the avatar head and body layers to hide - local player only
    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    // Executed for all remote players - Instantiates selected avatar model then parents the parts to the assigned vr player transforms
    [PunRPC]
    public void InitializeSelectedAvatarModel(int avatarSelectionNumber)
    {
        // Instantiate avatar model - child of main avatar
        GameObject selectedAvatarGameobject = Instantiate(avatarModelPrefabs[avatarSelectionNumber],mainAvatar.transform);
        
        // Avatar Holder is located on the avatar prefab
        AvatarHolder avatarHolder = selectedAvatarGameobject.GetComponent<AvatarHolder>();
        
        // Re parent instantiated body parts to existing vr player transform placeholders
        SetUpAvatarGameobject(avatarHolder.HeadTransform,xrTeleportControlSwitcher.avatarHead);            
        SetUpAvatarGameobject(avatarHolder.BodyTransform, xrTeleportControlSwitcher.avatarBody);
        SetUpAvatarGameobject(avatarHolder.HandLeftTransform,xrTeleportControlSwitcher.avatarLeftHand);
        SetUpAvatarGameobject(avatarHolder.HandRightTransform,xrTeleportControlSwitcher.avatarRightHand);
        // should delete original object (main avatar transform)
    }
    
    void SetUpAvatarGameobject(Transform avatarPart, Transform newParent)
    {
        // Make child of main avatar transform e.g place head model as child of Main Avatar / Head GO
        avatarPart.SetParent(newParent);
        avatarPart.localPosition = Vector3.zero;
        avatarPart.localRotation = Quaternion.identity;
    }
}
