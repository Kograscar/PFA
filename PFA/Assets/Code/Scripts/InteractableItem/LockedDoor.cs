using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : InteractableItem
{
    [SerializeField] Goal _goal;
    bool _close = true;
    KeyManager _keyManager;
    [SerializeField] int _neededKeys;


    void Start(){
        _goal = GetComponent<Goal>();
        _keyManager = GameObject.FindObjectOfType<KeyManager>();
    }

    public override void Use(GameObject player){
        if(_close){
            if(_keyManager._keys.Count >= _neededKeys){
                _goal.Solved();
                _close = false;
                gameObject.tag = "Untagged";
            }else{
            }
        }
    }
}
