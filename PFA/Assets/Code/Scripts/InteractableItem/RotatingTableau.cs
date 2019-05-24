using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTableau : InteractableItem
{
    float _lerpDelay;
    bool _interacting;
    Quaternion _baseQuaternion;
    Quaternion _futureQuaternion;
    Quaternion _addedRotation = Quaternion.Euler(0, 0, 180);

    void Update(){
        if(_interacting){
            _lerpDelay += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(_baseQuaternion, _futureQuaternion, _lerpDelay);
            if(_lerpDelay >= 1){
                _interacting = false;
                _lerpDelay = 0;
            }
        }
    }

    public override void Use(GameObject player){
        if(_interacting == false){
            _interacting = true;
            _baseQuaternion = transform.rotation;
            _futureQuaternion = _baseQuaternion * _addedRotation;
        }
    }
}
