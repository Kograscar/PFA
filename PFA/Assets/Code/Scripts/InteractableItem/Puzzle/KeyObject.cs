using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    public TypeOfKeyObject _keyObject;
    public ColorEnum _color;
    [HideInInspector] public List<GameObject> _colliders;
    [HideInInspector] public List<PickUp> _pickUps;
    public bool _rightGuess;
    public float _keyObjectNumber;
    public bool _solved;
    
    void OnTriggerEnter(Collider other){
        if(_solved == false){
            switch(_keyObject){
                case TypeOfKeyObject.Receptacle :
                        _pickUps.Add(other.GetComponent<PickUp>());
                        _pickUps[0].Place(transform);
                        _colliders.Add(other.gameObject);
                    break;

                case TypeOfKeyObject.TruDat :
                    if(other.CompareTag("RightBloc") || other.CompareTag("Token")){
                        _rightGuess = true;
                    }
                    break;
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(_solved == false){
            switch(_keyObject){
                case TypeOfKeyObject.Receptacle :

                    if(_colliders.Count > 0){
                        _pickUps[0].Unplace();
                        _colliders.Remove(other.gameObject);
                        _pickUps.Remove(other.gameObject.GetComponent<PickUp>());
                        _rightGuess = false;
                    }
                    
                    break;

                case TypeOfKeyObject.TruDat :

                    _rightGuess = false;

                    break;
            }
        }
    }

    void OnTriggerStay(Collider other){
        if(_solved == false){
            switch(_keyObject){
                case TypeOfKeyObject.Receptacle :
                    if(Input.GetMouseButtonDown(0)){
                        if(_colliders.Count > 0){
                            _colliders[0].transform.parent = transform;
                            _colliders[0].transform.localPosition = Vector3.zero;
                            _colliders[0].transform.rotation = transform.rotation;
                            _pickUps[0].Fix();
                            if(_pickUps[0]._pickUpNumber == _keyObjectNumber){
                                _rightGuess = true;
                            }else{
                                _rightGuess = false;
                            }
                        }
                    }
                    break;
            }
        }
    }
}
