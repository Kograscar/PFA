using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioClips : MonoBehaviour
{
    public AudioSource _Clips;

    public AudioClip[] audioClipArray;

    void Awake()
    {
        _Clips = GetComponent<AudioSource>();
    }

    void Start()
    {
        _Clips.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        
    }
}
