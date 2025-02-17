﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damier : MonoBehaviour
{
    [SerializeField] Transform[] _horizontalTransform;
    [SerializeField] Transform[] _verticalTransform;
    float[] _XArray;
    float[] _YArray;
    int _tokenXPos = 0;
    int _tokenYPos = 0;
    [SerializeField] Transform _token;
    Vector3 _actualPosition;
    Vector3 _futurePosition;
    float _lerpDelay = 0;


    void Start(){
        _XArray = new float[_horizontalTransform.Length];
        _YArray = new float[_verticalTransform.Length];
        for (int i = 0; i < _horizontalTransform.Length; i++){
            _XArray[i] = _horizontalTransform[i].localPosition.x;
        }

        for (int i = 0; i < _verticalTransform.Length; i++){
            _YArray[i] = _verticalTransform[i].localPosition.y;
        }
        _actualPosition = _token.localPosition;
        _futurePosition = new Vector3(_XArray[_tokenXPos], _YArray[_tokenYPos], _token.localPosition.z);
        _lerpDelay = 0;
    }

    void Update(){
        _lerpDelay += Time.deltaTime * 5;
        _token.localPosition = Vector3.Lerp(_actualPosition, _futurePosition, _lerpDelay);
    }

    public void MoveToken(MoveTokenDirection tokenDirection, int multiplicatorValue){
        if(_lerpDelay >= 1){
            switch (tokenDirection){
                case MoveTokenDirection.Horizontal :
                        _tokenXPos += 1 * multiplicatorValue;
                        if(_tokenXPos <= 0){
                            _tokenXPos = 0;
                        }
                        if(_tokenXPos >= _XArray.Length){
                            _tokenXPos = _XArray.Length-1;
                        }
                    break;
                
                case MoveTokenDirection.Vertical :
                        _tokenYPos += 1 * multiplicatorValue;
                        if(_tokenYPos <= 0){
                            _tokenYPos = 0;
                        }
                        if(_tokenYPos >= _YArray.Length){
                            _tokenYPos = _YArray.Length-1;
                        }
                    break;
            }
            _lerpDelay = 0;
            _actualPosition = _token.localPosition;
            _futurePosition = new Vector3(_XArray[_tokenXPos], _YArray[_tokenYPos], _token.localPosition.z);
        }
    }

    public void ResetToken(){
        _tokenXPos = 0;
        _tokenYPos = 0;
        _futurePosition = new Vector3(_XArray[_tokenXPos], _YArray[_tokenYPos], _token.localPosition.z);
    }
}
