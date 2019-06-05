using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : InteractableItem
{
    [HideInInspector] public enum ColorEnum{Blue, Red, Yellow, Purple, Green, Orange};
    public ColorEnum _color;
    Transform _itemCanvas;
    CharController _charController;
    [SerializeField] Rigidbody _rigidbody;
    bool _snapped;
    public float _pickUpNumber;

    public override void Use(GameObject player){
        _itemCanvas = player.transform;
        _charController = player.GetComponentInParent<CharController>();
        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public override void UnUse(){
        _rigidbody.useGravity = true;
        _rigidbody.freezeRotation = false;
        _rigidbody.constraints = RigidbodyConstraints.None;
    }

    public void Place(Transform papa){
        if(_snapped == false){
            transform.position = papa.position;
            _snapped = true;
        }
    }

    public void Unplace(){
        _snapped = false;
        transform.localPosition = Vector3.zero;
    }

    public void Fix(){
        _charController._carryingItem = false;
    }
}
