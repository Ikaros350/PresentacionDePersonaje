using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource mySource;
    [SerializeField] AudioClip[] clips;

    void Awake()
    {
        mySource = GetComponent<AudioSource>();
    }

    public void PlayLoop(int i)
    {
        if (!mySource.isPlaying)
        {
            mySource.clip = clips[i];
            mySource.Play();
        }
    }
    public void PlayAction(int i)
    {
        mySource.PlayOneShot(clips[i]);
    }

    public void Stop()
    {
        mySource.Stop();
    }
}
