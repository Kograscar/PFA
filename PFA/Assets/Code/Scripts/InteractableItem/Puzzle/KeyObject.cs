using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    public TypeOfKeyObject _keyObject;
    public PickUpType _pickUpType;
    [HideInInspector] public List<GameObject> _colliders;
    [HideInInspector] public List<PickUp> _pickUps;
    public bool _rightGuess;
    public bool _solved;
    
    void OnTriggerEnter(Collider other){
        if(_solved == false){
            switch(_keyObject){
                case TypeOfKeyObject.Receptacle :
                if(other.gameObject.CompareTag("CanSnap")){
                        _pickUps.Add(other.GetComponent<PickUp>());
                        _pickUps[0].Place(transform);
                        _colliders.Add(other.gameObject);
                        
                }
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
                    if(other.gameObject.CompareTag("CanSnap")){
                        if(Input.GetMouseButtonDown(0)){
                            if(_colliders.Count > 0){
                                _colliders[0].transform.parent = transform;
                                _colliders[0].transform.localPosition = Vector3.zero;
                                _colliders[0].transform.rotation = transform.rotation;
                                _pickUps[0].Fix();
                                _pickUps[0].gameObject.tag = "InteractableItem";
                                if(_pickUps[0]._pickUpType == _pickUpType){
                                    _rightGuess = true;
                                }else{
                                    _rightGuess = false;
                            }
                        }
                    }
                }
                    break;
            }
        }
    }
}
