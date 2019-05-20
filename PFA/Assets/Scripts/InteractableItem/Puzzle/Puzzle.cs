using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : InteractableItem
{
    [SerializeField] List<KeyObject> _keyObjects;
    int _rightGuessed;
    bool _solved = false;
    [SerializeField] Goal _goal;

    void Update(){
        Solve();
    }
    
    void Solve(){
        if(_keyObjects.Count != 0){
            if(_solved == false){
                foreach(var item in _keyObjects){
                    if(item._rightGuess == true){
                        _rightGuessed++;
                    }
                }
                if(_rightGuessed == _keyObjects.Count){
                    _goal.Solved();
                    _solved = true;
                }else{
                    _rightGuessed = 0;
                }
            }
        }else if(_keyObjects.Count != 0){
            if(_solved == false){
                foreach(var item in _keyObjects){
                    if(item._rightGuess == true){
                        _rightGuessed++;
                    }
                }
                if(_rightGuessed == _keyObjects.Count){
                    _goal.Solved();
                    _solved = true;
                }else{
                    _rightGuessed = 0;
                }
            }
        }
    }
}
