using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioClips : MonoBehaviour
{
    public AudioSource _audioSource;

    public AudioClip[] audioClipArray;
    AudioClip _actualAudioClip;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        AssignAudioClip();

    }

    void AssignAudioClip()
    {
        _actualAudioClip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        _audioSource.clip = _actualAudioClip;
        _audioSource.Play();
        StartCoroutine(DelayZizic());
    }

    IEnumerator DelayZizic()
    {
        yield return new WaitForSeconds(_actualAudioClip.length);
        AssignAudioClip();
    }

   
}
