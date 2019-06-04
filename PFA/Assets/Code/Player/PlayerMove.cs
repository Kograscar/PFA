using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string _horizontalInputName;
    [SerializeField] private string _verticalInputName;
    [SerializeField] private float _moveSpeed;
    private CharacterController _characterController;
    [SerializeField] AudioSource _audioSource;

    public AudioClip[] _audioClipArray;
    AudioClip _actualAudioClip;
    Vector3 _movement;

    private void Awake()
    {
        _characterController = GetComponentInChildren<CharacterController>();
		_characterController = GetComponent<CharacterController>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = Random.Range(0.8f, 1);
        _audioSource.pitch = Random.Range(1f, 1f);
    }

    private void Update()
    {
        PlayerMovement();
        if(_characterController.isGrounded == true && _movement != Vector3.zero && _audioSource.isPlaying == false)
		{
            AssignAudioClip();
        }
    }

    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(_horizontalInputName) * _moveSpeed * Time.deltaTime;
        float verInput = Input.GetAxis(_verticalInputName) * _moveSpeed * Time.deltaTime;

        /*Vector3 forwardMovement = transform.forward * verInput;
        Vector3 rightMovement = transform.right * horizInput;*/
        _movement = transform.forward * verInput + transform.right * horizInput;

        _characterController.SimpleMove(_movement);
    }

    void AssignAudioClip()
    {
        _actualAudioClip = _audioClipArray[Random.Range(0, _audioClipArray.Length)];
        _audioSource.clip = _actualAudioClip;
        _audioSource.Play();
        //StartCoroutine(DelayZizic());
    }

    /*IEnumerator DelayZizic()
    {
        yield return new WaitForSeconds(_actualAudioClip.length);
    }*/
}
