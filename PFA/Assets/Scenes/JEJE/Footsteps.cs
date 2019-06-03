using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {

	CharacterController _characterController;
	public AudioSource _sourceAudio ;

	void Start ()	
 	{
		_characterController = GetComponent<CharacterController>();
        _sourceAudio = GetComponent<AudioSource>();

    }
	
	void Update ()	
 	{
		if(_characterController.isGrounded == true && _characterController.velocity.magnitude > 2f && _sourceAudio.isPlaying == false)
		{
            _sourceAudio.volume = Random.Range(0.8f, 1);
            _sourceAudio.pitch = Random.Range(0.8f, 1.1f);
            _sourceAudio.Play();
        }
	}
}
