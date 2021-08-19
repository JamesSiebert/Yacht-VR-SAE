using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetskiDoorController : MonoBehaviour
{
    // Animators
    public bool jetskiDown = false;
    public Animator jetskiDoor;
    public Animator jetskiMove;
    
    public AudioSource audioJetskiDown;
    public AudioSource audioJetskiUp;
    
    // For VR Toggle
    public void JetskiToggle()
    {
        if (jetskiDown)
            JetskiUp();
        else
            JetskiDown();
    }
    
    // Animation - open door and move jetski down
    public void JetskiDown()
    {
        jetskiDoor.SetBool("jetskiDown", true);
        jetskiMove.SetBool("jetskiDown", true);
        audioJetskiDown.PlayOneShot(audioJetskiDown.clip);
        jetskiDown = true;
    }
    
    public void JetskiUp()
    {
        jetskiDoor.SetBool("jetskiDown", false);
        jetskiMove.SetBool("jetskiDown", false);
        audioJetskiUp.PlayOneShot(audioJetskiUp.clip);
        jetskiDown = false;
    }

}
