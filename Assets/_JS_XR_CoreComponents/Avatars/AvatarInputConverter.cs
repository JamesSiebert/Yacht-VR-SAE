using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarInputConverter : MonoBehaviour
{

    //Avatar Transforms
    public Transform MainAvatarTransform;
    public Transform AvatarHead;
    public Transform AvatarBody;

    public Transform AvatarHand_Left;
    public Transform AvatarHand_Right;

    //XRRig Transforms
    public Transform XR_Camera;

    public Transform XR_LeftBaseController;
    public Transform XR_RightBaseController;

    public Vector3 headPositionOffset;
    public Vector3 handRotationOffset;

    

    // Update is called once per frame
    void Update()
    {
        //Head and Body synch
        MainAvatarTransform.position = Vector3.Lerp(MainAvatarTransform.position, XR_Camera.position + headPositionOffset, 0.5f);
        AvatarHead.rotation = Quaternion.Lerp(AvatarHead.rotation, XR_Camera.rotation, 0.5f);
        AvatarBody.rotation = Quaternion.Lerp(AvatarBody.rotation, Quaternion.Euler(new Vector3(0, AvatarHead.rotation.eulerAngles.y, 0)), 0.05f);

        
        AvatarHand_Right.position = Vector3.Lerp(AvatarHand_Right.position,XR_RightBaseController.position,0.5f);
        AvatarHand_Right.rotation = Quaternion.Lerp(AvatarHand_Right.rotation,XR_RightBaseController.rotation,0.5f)*Quaternion.Euler(handRotationOffset);

        AvatarHand_Left.position = Vector3.Lerp(AvatarHand_Left.position,XR_LeftBaseController.position,0.5f);
        AvatarHand_Left.rotation = Quaternion.Lerp(AvatarHand_Left.rotation,XR_LeftBaseController.rotation,0.5f)*Quaternion.Euler(handRotationOffset);
    }
}
