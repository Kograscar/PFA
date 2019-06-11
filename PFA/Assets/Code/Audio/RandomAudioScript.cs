using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioScript : MonoBehaviour
{
    public AudioClip[] clips;
    [SerializeField] private AudioSource audioSource;

   
    private AudioClip GetRandomClip ()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }

    }
}

