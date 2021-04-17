using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;


public class JS_XRInputEventTrigger : MonoBehaviour
{
    public bool showDebugMessages;
    
    // User settings
    public bool trackLeftPrimaryButton;
    public bool leftPrimaryButtonPressed;    // Used to track release
    public UnityEvent OnLeftPrimaryPress;
    public UnityEvent OnLeftPrimaryRelease;
    
    public bool trackLeftSecondaryButton;
    public bool leftSecondaryButtonPressed;    // Used to track release
    public UnityEvent OnLeftSecondaryPress;
    public UnityEvent OnLeftSecondaryRelease;
    
    public bool trackLeftGripButton;
    public bool leftGripButtonPressed;    // Used to track release
    public UnityEvent OnLeftGripPress;
    public UnityEvent OnLeftGripRelease;
    
    public bool trackLeftTriggerButton;
    public bool leftTriggerButtonPressed;    // Used to track release
    public UnityEvent OnLeftTriggerPress;
    public UnityEvent OnLeftTriggerRelease;
    
    public bool trackLeftMenuButton;
    public bool leftMenuButtonPressed;    // Used to track release
    public UnityEvent OnLeftMenuPress;
    public UnityEvent OnLeftMenuRelease;
    
    
    public bool trackRightPrimaryButton;
    public bool rightPrimaryButtonPressed;    // Used to track release
    public UnityEvent OnRightPrimaryPress;
    public UnityEvent OnRightPrimaryRelease;
    
    public bool trackRightSecondaryButton;
    public bool rightSecondaryButtonPressed;    // Used to track release
    public UnityEvent OnRightSecondaryPress;
    public UnityEvent OnRightSecondaryRelease;
    
    public bool trackRightGripButton;
    public bool rightGripButtonPressed;    // Used to track release
    public UnityEvent OnRightGripPress;
    public UnityEvent OnRightGripRelease;
    
    public bool trackRightTriggerButton;
    public bool rightTriggerButtonPressed;    // Used to track release
    public UnityEvent OnRightTriggerPress;
    public UnityEvent OnRightTriggerRelease;
    
    

    public XRNode leftXrNode = XRNode.LeftHand;
    public XRNode rightXrNode = XRNode.RightHand;

    public List<InputDevice> leftDevices = new List<InputDevice>();
    public List<InputDevice> rightDevices = new List<InputDevice>();
    public InputDevice leftDevice;
    public InputDevice rightDevice;


    public Vector3 leftControllerPosition;
    public Vector3 rightControllerPosition;
    public Quaternion leftControllerRotation;
    public Quaternion rightControllerRotation;
    
    
    void GetLeftDevice()
    {
        InputDevices.GetDevicesAtXRNode(leftXrNode, leftDevices);
        leftDevice = leftDevices.FirstOrDefault();
    }
    
    void GetRightDevice()
    {
        InputDevices.GetDevicesAtXRNode(rightXrNode, rightDevices);
        rightDevice = rightDevices.FirstOrDefault();
    }

    private void OnEnable()
    {
        if (!leftDevice.isValid)
        {
            GetLeftDevice();
        }
        
        if (!rightDevice.isValid)
        {
            GetRightDevice();
        }
    }

    void Update()
    {

        if (!leftDevice.isValid)
            GetLeftDevice();
        
        if (!rightDevice.isValid)
            GetRightDevice();



            // Read Rotation
            InputFeatureUsage<Quaternion> leftControllerRot = CommonUsages.deviceRotation;
            InputFeatureUsage<Quaternion> rightControllerRot = CommonUsages.deviceRotation;
            leftDevice.TryGetFeatureValue(leftControllerRot, out leftControllerRotation);
            rightDevice.TryGetFeatureValue(rightControllerRot, out rightControllerRotation);
            
            // Read Position
            InputFeatureUsage<Vector3> leftControllerPos = CommonUsages.devicePosition;
            InputFeatureUsage<Vector3> rightControllerPos = CommonUsages.devicePosition;
            leftDevice.TryGetFeatureValue(leftControllerPos, out leftControllerPosition);
            rightDevice.TryGetFeatureValue(rightControllerPos, out rightControllerPosition);


        

        
        if (trackLeftPrimaryButton)
        {
            bool value = false;
            InputFeatureUsage<bool> usage = CommonUsages.primaryButton;

            // If button is being pressed
            if (leftDevice.TryGetFeatureValue(usage, out value) && value)
            {
                // If this is the first call while being pressed
                if (!leftPrimaryButtonPressed)
                {
                    // Run this once
                    if(showDebugMessages){Debug.Log("Left Primary Pressed");}
                    leftPrimaryButtonPressed = true;
                    OnLeftPrimaryPress.Invoke();
                }
            }
            //
            else if (leftPrimaryButtonPressed) // If released
            {
                if(showDebugMessages){Debug.Log("Left Primary Released");}
                leftPrimaryButtonPressed = false;
                OnLeftPrimaryRelease.Invoke();
            }
        }

        if (trackLeftSecondaryButton)
        {
            bool value = false;
            InputFeatureUsage<bool> usage = CommonUsages.secondaryButton;

            if (leftDevice.TryGetFeatureValue(usage, out value) && value)
            {
                if (!leftSecondaryButtonPressed) // If start press
                {
                    if(showDebugMessages){Debug.Log("Left Secondary Pressed");}
                    leftSecondaryButtonPressed = true;
                    OnLeftSecondaryPress.Invoke();
                }
            }
            else if (leftSecondaryButtonPressed) // If released
            {
                if(showDebugMessages){Debug.Log("Left Secondary Released");}
                leftSecondaryButtonPressed = false;
                OnLeftSecondaryRelease.Invoke();
            }
        }

        if (trackLeftGripButton)
        {
            bool value = false;
            InputFeatureUsage<bool> usage = CommonUsages.gripButton;

            if (leftDevice.TryGetFeatureValue(usage, out value) && value)
            {
                if (!leftGripButtonPressed) // If start press
                {
                    if(showDebugMessages){Debug.Log("Left Grip Pressed");}
                    leftGripButtonPressed = true;
                    OnLeftGripPress.Invoke();
                }
            }
            else if (leftGripButtonPressed) // If released
            {
                if(showDebugMessages){Debug.Log("Left Grip Released");}
                leftGripButtonPressed = false;
                OnLeftGripRelease.Invoke();
            }
        }

        if (trackLeftTriggerButton)
        {
            bool value = false;
            InputFeatureUsage<bool> usage = CommonUsages.triggerButton;

            if (leftDevice.TryGetFeatureValue(usage, out value) && value)
            {
                if (!leftTriggerButtonPressed) // If start press
                {
                    if(showDebugMessages){Debug.Log("Left Trigger Pressed");}
                    leftTriggerButtonPressed = true;
                    OnLeftTriggerPress.Invoke();
                }
            }
            else if (leftTriggerButtonPressed) // If released
            {
                if(showDebugMessages){Debug.Log("Left Trigger Released");}
                leftTriggerButtonPressed = false;
                OnLeftTriggerRelease.Invoke();
            }
        }

        if (trackLeftMenuButton)
        {
            bool value = false;
            InputFeatureUsage<bool> usage = CommonUsages.menuButton;

            if (leftDevice.TryGetFeatureValue(usage, out value) && value)
            {
                if (!leftMenuButtonPressed) // If start press
                {
                    if(showDebugMessages){Debug.Log("Left Menu Pressed");}
                    leftMenuButtonPressed = true;
                    OnLeftMenuPress.Invoke();
                }
            }
            else if (leftMenuButtonPressed) // If released
            {
                if(showDebugMessages){Debug.Log("Left Menu Released");}
                leftMenuButtonPressed = false;
                OnLeftMenuRelease.Invoke();
            }
        }

        // -- Right Controller --

        if (trackRightPrimaryButton)
        {
            bool value = false;
            InputFeatureUsage<bool> usage = CommonUsages.primaryButton;

            if (rightDevice.TryGetFeatureValue(usage, out value) && value)
            {
                if (!rightPrimaryButtonPressed) // If start press
                {
                    if(showDebugMessages){Debug.Log("Right Primary Pressed");}
                    rightPrimaryButtonPressed = true;
                    OnRightPrimaryPress.Invoke();
                }
            }
            else if (rightPrimaryButtonPressed) // If released
            {
                if(showDebugMessages){Debug.Log("Right Primary Released");}
                rightPrimaryButtonPressed = false;
                OnRightPrimaryRelease.Invoke();
            }
        }

        if (trackRightSecondaryButton)
        {
            bool value = false;
            InputFeatureUsage<bool> usage = CommonUsages.secondaryButton;

            if (rightDevice.TryGetFeatureValue(usage, out value) && value)
            {
                if (!rightSecondaryButtonPressed) // If start press
                {
                    if(showDebugMessages){Debug.Log("Right Secondary Pressed");}
                    rightSecondaryButtonPressed = true;
                    OnRightSecondaryPress.Invoke();
                }
            }
            else if (rightSecondaryButtonPressed) // If released
            {
                if(showDebugMessages){Debug.Log("Right Secondary Released");}
                rightSecondaryButtonPressed = false;
                OnRightSecondaryRelease.Invoke();
            }
        }

        if (trackRightGripButton)
        {
            bool value = false;
            InputFeatureUsage<bool> usage = CommonUsages.gripButton;

            if (rightDevice.TryGetFeatureValue(usage, out value) && value)
            {
                if (!rightGripButtonPressed) // If start press
                {
                    if(showDebugMessages){Debug.Log("Right Grip Pressed");}
                    rightGripButtonPressed = true;
                    OnRightGripPress.Invoke();
                }
            }
            else if (rightGripButtonPressed) // If released
            {
                if(showDebugMessages){Debug.Log("Right Grip Released");}
                rightGripButtonPressed = false;
                OnRightGripRelease.Invoke();
            }
        }

        if (trackRightTriggerButton)
        {
            bool value = false;
            InputFeatureUsage<bool> usage = CommonUsages.triggerButton;

            if (rightDevice.TryGetFeatureValue(usage, out value) && value)
            {
                if (!rightTriggerButtonPressed) // If start press
                {
                    if(showDebugMessages){Debug.Log("Right Trigger Pressed");}
                    rightTriggerButtonPressed = true;
                    OnRightTriggerPress.Invoke();
                }
            }
            else if (rightTriggerButtonPressed) // If released
            {
                if(showDebugMessages){Debug.Log("Right Trigger Released");}
                rightTriggerButtonPressed = false;
                OnRightTriggerRelease.Invoke();
            }
        }
    }
}
