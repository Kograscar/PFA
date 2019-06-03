using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {

	CharacterController _characterController;
    [SerializeField] AudioSource _audioSource;

    public AudioClip[] audioClipArray;
    AudioClip _actualAudioClip;

    void Start ()	
 	{
		_characterController = GetComponent<CharacterController>();
        _audioSource = GetComponent<AudioSource>();

    }
	
	void Update ()	
 	{
		if(_characterController.isGrounded == true && _characterController.velocity.magnitude > 2f && _audioSource.isPlaying == false)
		{
            _audioSource.volume = Random.Range(0.8f, 1);
            _audioSource.pitch = Random.Range(1f, 1f);
            AssignAudioClip();
        }
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
