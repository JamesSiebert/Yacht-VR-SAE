using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlaySound : MonoBehaviour
{

    public AudioSource audioSource;

    public void PlaySound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
