using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {

	CharacterController cc;
	public AudioSource SourceAudio ;

	void Start ()	
 	{
		cc = GetComponent<CharacterController>();
        GetComponent<AudioSource>();

    }
	
	void Update ()	
 	{
		if(cc.isGrounded == true && cc.velocity.magnitude > 2f && GetComponent<AudioSource>().isPlaying == false)
		{
            GetComponent<AudioSource>().volume = Random.Range(0.8f, 1);
            GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.1f);
            GetComponent<AudioSource>().Play();
        }
	}
}
