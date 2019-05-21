using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    Damier _damier;
    Vector3 _lastPosition;
    bool _pranked;

    void Start(){
        _damier = GetComponentInParent<Damier>();
    }

    void Update(){
        if(_pranked == true){
            if(_lastPosition == transform.position){
                _damier.ResetToken();
                _pranked = false;
            }
        }
        _lastPosition = transform.position;
    }

    public void YouVeBeenPranked(){
        _pranked = true;
    }
}