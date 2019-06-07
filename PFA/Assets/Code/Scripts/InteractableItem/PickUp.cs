using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : InteractableItem
{
    public ColorEnum _color;
    Transform _itemCanvas;
    CharController _charController;
    [SerializeField] Rigidbody _rigidbody;
    bool _snapped;


    void Start(){
        _rigidbody = GetComponent<Rigidbody>();
    }

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
            transform.rotation = papa.rotation;
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
