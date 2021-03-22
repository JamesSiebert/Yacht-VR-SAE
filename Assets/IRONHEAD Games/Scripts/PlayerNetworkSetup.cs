using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;


public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{

    public GameObject LocalXRRigGameobject;
    public GameObject MainAvatarGameobject;

    public GameObject AvatarHeadGameobject;
    public GameObject AvatarBodyGameobject;

    public GameObject[] AvatarModelPrefabs;

    public TextMeshProUGUI PlayerName_Text;

    void Start()
    {
        // Setup player
        if(photonView.IsMine)
        {
            // Local Player
            LocalXRRigGameobject.SetActive(true);
            gameObject.GetComponent<MovementController>().enabled = true;
            gameObject.GetComponent<AvatarInputConverter>().enabled = true;

            // Get avatar selection data so we can instantiate correct avatar model.
            object avatarSelectionNumber;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerVRConstants.AVATAR_SELECTION_NUMBER, out avatarSelectionNumber))
            {
                Debug.Log("Avatar selection number: " + (int)avatarSelectionNumber);
                photonView.RPC("InitializeSelectedAvatarModel", RpcTarget.AllBuffered, (int)avatarSelectionNumber);
            }


            // Hide local player head and body
            SetLayerRecursively(AvatarHeadGameobject, 11);
            SetLayerRecursively(AvatarBodyGameobject, 12);

            // Finds object that have teleportation component
            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();
            if(teleportationAreas.Length > 0)
            {
                Debug.Log("Found " + teleportationAreas.Length + " teleporation areas.");
                foreach (var item in teleportationAreas)
                {
                    // Assigns the spawned XR Rigs teleporation provider to each teleporation area
                    item.teleportationProvider = LocalXRRigGameobject.GetComponent<TeleportationProvider>();
                }
            }

            // Add audio listener to avatar - only 1 audio listener in scene
            MainAvatarGameobject.AddComponent<AudioListener>();
        }
        else
        {
            // Remote Player
            LocalXRRigGameobject.SetActive(false);
            gameObject.GetComponent<MovementController>().enabled = false;
            gameObject.GetComponent<AvatarInputConverter>().enabled = false;

            SetLayerRecursively(AvatarHeadGameobject, 0);
            SetLayerRecursively(AvatarBodyGameobject, 0);
        }

        if(PlayerName_Text != null)
        {
            PlayerName_Text.text = photonView.Owner.NickName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Sets the avar head and body layers to hide - local player only
    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    // Executed for all remote players
    [PunRPC]
    public void InitializeSelectedAvatarModel(int avatarSelectionNumber)
    {
        // Instantiate avatar model - child of LocalXRRigGameobject
        GameObject selectedAvatarGameobject = Instantiate(AvatarModelPrefabs[avatarSelectionNumber],LocalXRRigGameobject.transform);

        AvatarInputConverter avatarInputConverter = transform.GetComponent<AvatarInputConverter>();
        AvatarHolder avatarHolder = selectedAvatarGameobject.GetComponent<AvatarHolder>();

        SetUpAvatarGameobject(avatarHolder.HeadTransform,avatarInputConverter.AvatarHead);
        SetUpAvatarGameobject(avatarHolder.BodyTransform,avatarInputConverter.AvatarBody);
        SetUpAvatarGameobject(avatarHolder.HandLeftTransform, avatarInputConverter.AvatarHand_Left);
        SetUpAvatarGameobject(avatarHolder.HandRightTransform, avatarInputConverter.AvatarHand_Right);
    }

    void SetUpAvatarGameobject(Transform avatarModelTransform, Transform mainAvatarTransform)
    {
        // Make child of main avatar transform e.g place head model as child of Main Avatar / Head GO
        avatarModelTransform.SetParent(mainAvatarTransform);
        avatarModelTransform.localPosition = Vector3.zero;
        avatarModelTransform.localRotation = Quaternion.identity;
    }

}
