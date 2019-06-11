using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSFX : MonoBehaviour
{
    [SerializeField] Transform _pointA;
    [SerializeField] Transform _pointB;
    bool _rushing = true;
    [SerializeField] float _multiplicator;
    float _lerpDelay;

    void Start(){
        transform.position = _pointA.position;
    }

    void Update()
    {
        _lerpDelay += Time.deltaTime * _multiplicator;
        if(_rushing){
            transform.position = Vector3.Lerp(_pointA.position, _pointB.position, _lerpDelay);
        }else if(_rushing == false){
            transform.position = Vector3.Lerp(_pointB.position, _pointA.position, _lerpDelay);
        }
        if(_lerpDelay >= 1){
            _rushing = !_rushing;
        }
    }
}
