using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : InteractableItem
{
    public PickUpType _pickUpType;
    Transform _itemCanvas;
    CharController _charController;
    [SerializeField] Rigidbody _rigidbody;
    bool _snapped;
    [SerializeField] bool _animated;
    [SerializeField] Animation _animation;


    void Start(){
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Use(GameObject player){
        _itemCanvas = player.transform;
        _charController = player.GetComponentInParent<CharController>();
        _rigidbody.useGravity = false;
        transform.rotation = _charController._itemCanvas.transform.rotation;
        _rigidbody.freezeRotation = true;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        if(_animated){
            _animation.Play();
        }
    }

    public override void UnUse(){
        _rigidbody.useGravity = true;
        _rigidbody.freezeRotation = false;
        _rigidbody.constraints = RigidbodyConstraints.None;
        if(_animated){
            _animation.Rewind();
        }
    }

    public void Place(Transform papa){
        if(_pickUpType != PickUpType.Clue){
            if(_snapped == false){
                transform.position = papa.position;
                transform.rotation = papa.rotation;
                _snapped = true;
            }
        }
    }

    public void Unplace(){
        if(_pickUpType != PickUpType.Clue){
            _snapped = false;
            transform.localPosition = Vector3.zero;
        }
    }

    public void Fix(){
        if(_pickUpType != PickUpType.Clue){
            _charController._carryingItem = false;
        }
    }
}
