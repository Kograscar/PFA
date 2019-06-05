using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levier : MonoBehaviour
{
    public MoveTokenDirection _levierMoveDirection;
    Damier _damier;
    Examinate _examinateScript;
    [SerializeField] Transform[] _limits;
    float[] _limitsOneVector = new float[2];
    [SerializeField] GameObject _levier;
    BoxCollider _levierCollider;
    Vector3 _lastFrameLevierPosition;
    Vector3 _lastLimit;
    float _lerpDelay;
    bool _canMove = true;

    void Start(){
        _examinateScript = GetComponentInParent<Examinate>();
        _levierCollider = _levier.GetComponent<BoxCollider>();
        _damier = GetComponentInParent<Damier>();
        if(_levierMoveDirection == MoveTokenDirection.Horizontal){
            for(int i = 0; i < _limits.Length; i++){
                _limitsOneVector[i] = _limits[i].position.x;
            }
        }else{
            for(int i = 0; i < _limits.Length; i++){
                _limitsOneVector[i] = _limits[i].position.y;
            }
        }
        _lastFrameLevierPosition = _levier.transform.localPosition;
    }

    void Update(){
        #region Limits
        if(_lastFrameLevierPosition != _levier.transform.localPosition){
            switch(_levierMoveDirection){
                case MoveTokenDirection.Horizontal :
                    if(_levier.transform.localPosition.x < _limits[0].localPosition.x){
                        _lastLimit = _limits[0].localPosition;
                        TouchLimit(-1);
                    }else 
                    if(_levier.transform.localPosition.x > _limits[1].localPosition.x){
                        _lastLimit = _limits[1].localPosition;
                        TouchLimit(1);
                    }
                    break;
                
                case MoveTokenDirection.Vertical :
                    if(_levier.transform.localPosition.y < _limits[0].localPosition.y){
                        _lastLimit = _limits[0].localPosition;
                        TouchLimit(-1);
                    }else 
                    if(_levier.transform.localPosition.y > _limits[1].localPosition.y){
                        _lastLimit = _limits[1].localPosition;
                        TouchLimit(1);
                    }
                    break;
            }
        }
        #endregion Limits
        if(_canMove == false){
            _lerpDelay += Time.deltaTime * 5f;
            _levier.transform.localPosition = Vector3.Lerp(_lastLimit, Vector3.zero, _lerpDelay);
            if(_lerpDelay >= 1 || _lastFrameLevierPosition == _levier.transform.localPosition){
                _canMove = true;
            }
        }
        _lastFrameLevierPosition = _levier.transform.localPosition;
    }

    void TouchLimit(int multiplicator){
        _examinateScript.DeselectingMesh();
        _canMove = false;
        _lerpDelay = 0;
        _damier.MoveToken(_levierMoveDirection, multiplicator);
    }
}
