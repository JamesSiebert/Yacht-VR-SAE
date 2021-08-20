using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{

    public bool tableDown = false;
    public Animator tabletop;
    public Animator tableLeg1;
    public Animator tableLeg2;
    public float animationDuration = 5.0f;

    public AudioSource audioTableDown;
    public AudioSource audioTableUp;
    
    public bool inProgress = false;
    
    public void TableToggle()
    {
        if (!inProgress)
        {
            if (tableDown)
                TableUp();
            else
                TableDown();
        }
    }
    
    // Animation - open door and move jetski down
    public void TableDown()
    {
        tabletop.SetBool("tableDown", true);
        tableLeg1.SetBool("tableDown", true);
        tableLeg2.SetBool("tableDown", true);
        audioTableDown.PlayOneShot(audioTableDown.clip);
        tableDown = true;
        // fixes frame skip issues
        StartCoroutine(AnimationCoroutine());
    }
    
    public void TableUp()
    {
        tabletop.SetBool("tableDown", false);
        tableLeg1.SetBool("tableDown", false);
        tableLeg2.SetBool("tableDown", false);
        audioTableUp.PlayOneShot(audioTableUp.clip);
        tableDown = false;
        // fixes frame skip issues
        StartCoroutine(AnimationCoroutine());
    }
    
    
    IEnumerator AnimationCoroutine()
    {
        inProgress = true;
        yield return new WaitForSeconds(animationDuration);
        inProgress = false;
    }
}
