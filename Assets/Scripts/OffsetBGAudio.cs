using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetBGAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public float startTime = 0f;
    
    void Start()
    {
        audioSource.time = startTime;
        audioSource.Play();
    }
}
