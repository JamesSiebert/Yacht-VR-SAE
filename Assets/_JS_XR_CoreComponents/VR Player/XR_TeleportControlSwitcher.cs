using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_TeleportControlSwitcher : MonoBehaviour
{
    /*
     *  NOTES: Issues with action based controllers, possible fix is to override the position with JS_XRInputEventTrigger positions but everything needs to be coded in thumbs, grab, teleport etc.
     *  DeviceBased controllers works better with Pun2
     */
    
    public GameObject leftDirectController;
    public GameObject leftRayController;
    public XRRayInteractor leftRayInteractor;
    
    public GameObject rightDirectController;
    public GameObject rightRayController;
    public XRRayInteractor rightRayInteractor;

    public bool teleportOnDeactivate;
    public bool teleportOnTrigger;
    public bool useDirectionalTeleporting;
    public GameObject directionalReticlePrefab;
    public JS_XRInputEventTrigger XRInputEventTriggerRef;    // get controller rotations
    [SerializeField] private TeleportationProvider provider;
    
    private GameObject leftDirectionalMarkerRef;
    private GameObject rightDirectionalMarkerRef;
    public bool leftTeleportActive;
    public bool rightTeleportActive;
    private float rotationAngleMultiplier = 3;
    private Quaternion leftTeleportRotation;
    private Quaternion rightTeleportRotation;

    // Local master or remote copy (networking)
    public bool thisIsMaster = false;
    
    public GameObject xrRig;
    public Transform XR_Camera;

    public Transform mainAvatar;
    public Transform avatarHead = null;
    public Transform avatarBody;
    public Transform avatarLeftHand;
    public Transform avatarRightHand;

    public Vector3 headPositionOffset = new Vector3(0,-0.9f,0);

    private int teleportationLayer;

    private void Start()
    {
        teleportationLayer = LayerMask.NameToLayer("Teleportation");

        if (useDirectionalTeleporting)
            SpawnDirectionalMarkers();
    }

    private void Update()
    {

        if (leftTeleportActive)
        {
            if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit) && hit.transform.gameObject.layer == teleportationLayer)
            {
                leftDirectionalMarkerRef.SetActive(true);

                // Update directional marker position
                leftDirectionalMarkerRef.transform.position = hit.point;
                
                // Get Controller Rotation
                Quaternion leftControllerRot = XRInputEventTriggerRef.leftControllerRotation;
                
                
                // -- DIRECTIONAL MARKER --
                
                // Gets direction of hit point from controller
                Vector3 hitDirection = hit.point - leftRayController.transform.position;
                
                // Locked y to account for downwards angle
                Quaternion forwardHeading = Quaternion.LookRotation(new Vector3(hitDirection.x, 0, hitDirection.z), Vector3.up);
                
                // Convert to Vector3 and get y rotation only
                float forwardHeadingAngle = forwardHeading.eulerAngles.y;

                // Get controller rotation * multiplier, then offset looking heading 
                float newDirectionalYRot = (-leftControllerRot.eulerAngles.z * rotationAngleMultiplier) + forwardHeadingAngle;

                // Update variable for teleporter
                leftTeleportRotation = Quaternion.Euler(0, newDirectionalYRot, 0);
                
                // Set directional marker rotation Y Axis only
                leftDirectionalMarkerRef.transform.localRotation = leftTeleportRotation;
            }
            else
            {
                leftDirectionalMarkerRef.SetActive(false);
            }
        }

        if (rightTeleportActive)
        {
            if (rightRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit) && hit.transform.gameObject.layer == teleportationLayer)
            {
                rightDirectionalMarkerRef.SetActive(true);
                
                // Update directional marker position
                rightDirectionalMarkerRef.transform.position = hit.point;
                
                // Get Controller Rotation
                Quaternion rightControllerRot = XRInputEventTriggerRef.rightControllerRotation;
                
                
                // -- DIRECTIONAL MARKER --
                
                // Gets direction of hit point from controller
                Vector3 hitDirection = hit.point - rightRayController.transform.position;
                
                // Locked y to account for downwards angle
                Quaternion forwardHeading = Quaternion.LookRotation(new Vector3(hitDirection.x, 0, hitDirection.z), Vector3.up);
                
                // Convert to Vector3 and get y rotation only
                float forwardHeadingAngle = forwardHeading.eulerAngles.y;

                // Get controller rotation * multiplier, then offset looking heading 
                float newDirectionalYRot = (-rightControllerRot.eulerAngles.z * rotationAngleMultiplier) + forwardHeadingAngle;

                // Update variable for teleporter
                rightTeleportRotation = Quaternion.Euler(0, newDirectionalYRot, 0);
                
                // Set directional marker rotation Y Axis only
                rightDirectionalMarkerRef.transform.localRotation = rightTeleportRotation;
            }
            else
            {
                rightDirectionalMarkerRef.SetActive(false);
            }
        }

        if (thisIsMaster)
        {
            UpdateAvatarBodyPositions();
        }
        
    }
    
    
    void UpdateAvatarBodyPositions()
    {
        //Head and Body position
        mainAvatar.position = Vector3.Lerp(mainAvatar.position, XR_Camera.position + headPositionOffset, 0.5f);

        avatarHead.rotation = Quaternion.Lerp(avatarHead.rotation, XR_Camera.rotation, 0.5f);
        avatarBody.rotation = Quaternion.Lerp(avatarBody.rotation, Quaternion.Euler(new Vector3(0, avatarHead.rotation.eulerAngles.y, 0)), 0.05f);

        
        // Left Hand Position
        if (leftTeleportActive)
        {
            //  *** Ray Controller
            avatarLeftHand.position = Vector3.Lerp(avatarLeftHand.position,leftRayController.transform.position,0.5f);
            avatarLeftHand.rotation = Quaternion.Lerp(avatarLeftHand.rotation,leftRayController.transform.rotation,0.5f);
        }
        else
        {
            // *** Direct Interactor // not using local bypasses xr rig position alterations
            avatarLeftHand.position = Vector3.Lerp(avatarLeftHand.position,leftDirectController.transform.position, 0.5f);
            avatarLeftHand.rotation = Quaternion.Lerp(avatarLeftHand.rotation, leftDirectController.transform.rotation, 0.5f);
        }
    
        // Right Hand Position
        if (rightTeleportActive)
        {
            avatarRightHand.position = Vector3.Lerp(avatarRightHand.position,rightRayController.transform.position,0.5f);
            avatarRightHand.rotation = Quaternion.Lerp(avatarRightHand.rotation,rightRayController.transform.rotation,0.5f);
        }
        else
        {
            avatarRightHand.position = Vector3.Lerp(avatarRightHand.position,rightDirectController.transform.position,0.5f);
            avatarRightHand.rotation = Quaternion.Lerp(avatarRightHand.rotation, rightDirectController.transform.rotation, 0.5f);
        }
    }


    // Left Teleport Controller
    public void ActivateLeftTeleportController()
    {
        if (useDirectionalTeleporting)
            leftTeleportActive = true;
        
        leftDirectController.SetActive(false);
        leftRayController.SetActive(true);
    }
    public void DeactivateLeftTeleportController()
    {
        // Teleport on deactivate
        if(teleportOnDeactivate)
            InitiateLeftTeleport();
        
        // turn off directional marker
        leftTeleportActive = false;
        leftDirectionalMarkerRef.SetActive(false);
        
        leftDirectController.SetActive(true);
        leftRayController.SetActive(false);
    }
    
    
    // Right Teleport Controller
    public void ActivateRightTeleportController()
    {
        if (useDirectionalTeleporting)
            rightTeleportActive = true;
        
        rightDirectController.SetActive(false);
        rightRayController.SetActive(true);
        
    }
    public void DeactivateRightTeleportController()
    {
        // Teleport on deactivate
        if(teleportOnDeactivate)
            InitiateRightTeleport();
        
        // turn off directional marker
        rightTeleportActive = false;
        rightDirectionalMarkerRef.SetActive(false);
        
        rightDirectController.SetActive(true);
        rightRayController.SetActive(false);
    }

    public void LeftTriggerActivated()
    {
        // Left trigger pressed while teleport beam active
        if (teleportOnTrigger && XRInputEventTriggerRef.leftPrimaryButtonPressed)
        {
            // Initiate left teleport
            InitiateLeftTeleport();
        }
    }
    
    public void RightTriggerActivated()
    {
        // Right trigger pressed while teleport beam active
        if (teleportOnTrigger && XRInputEventTriggerRef.rightPrimaryButtonPressed)
        {
            // Initiate right teleport
            InitiateRightTeleport();
        }
    }
    
    
    
    public void InitiateLeftTeleport()
    {
        if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit) == false)
        {
            Debug.Log("teleport hit false");
        }
        else
        {

            if (hit.transform.gameObject.layer == teleportationLayer)
            {
                // valid teleport destination
                TeleportRequest request = new TeleportRequest()
                {
                    destinationPosition = hit.point
                    //destinationRotation = leftTeleportRotation, // Attempting to teleport with rotation - not working?
                    //matchOrientation = MatchOrientation.None,
                    //requestTime = EpochUnixTime()
                };
                // Process teleport
                provider.QueueTeleportRequest(request);
                
                // Not sure if this is the right way to do this but Snap-Turn uses a similar method.
                xrRig.transform.rotation = leftTeleportRotation;
                
                mainAvatar.transform.rotation = leftTeleportRotation;
            }
        }
    }
    
    public float EpochUnixTime()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        float currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
 
        return currentEpochTime;
    }
    
    public void InitiateRightTeleport()
    {
        if (rightRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit) == false)
        {
            Debug.Log("teleport hit false");
        }
        else
        {
            if (hit.transform.gameObject.layer == teleportationLayer)
            {
                // valid teleport destination
                TeleportRequest request = new TeleportRequest()
                {
                    destinationPosition = hit.point,
                    destinationRotation = rightTeleportRotation, // Attempting to teleport with rotation - not working?
                    matchOrientation = MatchOrientation.None,
                    requestTime = EpochUnixTime()
                };
                
                // Process teleport
                provider.QueueTeleportRequest(request);
                
                // Not sure if this is the right way to do this but Snap-Turn uses a similar method.
                xrRig.transform.rotation = rightTeleportRotation;

                mainAvatar.transform.rotation = rightTeleportRotation;
            }
        }
    }


    public void SpawnDirectionalMarkers()
    {
        leftDirectionalMarkerRef = Instantiate(directionalReticlePrefab, new Vector3(0,0,0), Quaternion.identity);
        leftDirectionalMarkerRef.SetActive(false);
        
        rightDirectionalMarkerRef = Instantiate(directionalReticlePrefab, new Vector3(0,0,0), Quaternion.identity);
        rightDirectionalMarkerRef.SetActive(false);
    }
}
