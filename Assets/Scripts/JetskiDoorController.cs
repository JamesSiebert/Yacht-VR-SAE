using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetskiDoorController : MonoBehaviour
{
    // Animators
    public bool jetskiDown = false;
    public bool inProgress = false;
    
    public Animator jetskiDoor;
    public Animator jetskiMove;
    public float animationDuration = 15.0f;
    
    public AudioSource audioJetskiDown;
    public AudioSource audioJetskiUp;

    
    
    // For VR Toggle
    public void JetskiToggle()
    {
        if (!inProgress)
        {
            if (jetskiDown)
                JetskiUp();
            else
                JetskiDown();
        }
    }
    
    // Animation - open door and move jetski down
    public void JetskiDown()
    {
        jetskiDoor.SetBool("jetskiDown", true);
        jetskiMove.SetBool("jetskiDown", true);
        audioJetskiDown.PlayOneShot(audioJetskiDown.clip);
        inProgress = true;
        jetskiDown = true;
        // fixes frame skip issues
        StartCoroutine(AnimationCoroutine());
    }
    
    public void JetskiUp()
    {
        jetskiDoor.SetBool("jetskiDown", false);
        jetskiMove.SetBool("jetskiDown", false);
        audioJetskiUp.PlayOneShot(audioJetskiUp.clip);
        inProgress = true;
        jetskiDown = false;
        // fixes frame skip issues
        StartCoroutine(AnimationCoroutine());
    }

    IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(animationDuration);
        inProgress = false;
    }

}
