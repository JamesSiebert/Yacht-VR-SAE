using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{

    public bool tableDown = false;
    public Animator tabletop;
    public Animator tableLeg1;
    public Animator tableLeg2;

    public AudioSource audioTableDown;
    public AudioSource audioTableUp;
    
    public void TableToggle()
    {
        if (tableDown)
            TableUp();
        else
            TableDown();
    }
    
    // Animation - open door and move jetski down
    public void TableDown()
    {
        tabletop.SetBool("tableDown", true);
        tableLeg1.SetBool("tableDown", true);
        tableLeg2.SetBool("tableDown", true);
        audioTableDown.PlayOneShot(audioTableDown.clip);
        tableDown = true;
    }
    
    public void TableUp()
    {
        tabletop.SetBool("tableDown", false);
        tableLeg1.SetBool("tableDown", false);
        tableLeg2.SetBool("tableDown", false);
        audioTableUp.PlayOneShot(audioTableUp.clip);
        tableDown = false;
    }
}
