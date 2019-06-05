using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : InteractableItem
{
    [SerializeField] List<KeyObject> _keyObjects;
    int _rightGuessed;
    bool _solved = false;
    [SerializeField] Goal _goal;
    Examinate _examinate;
    CharController _char;

    void Start(){
        _examinate = GetComponentInChildren<Examinate>();
        _char = Camera.main.gameObject.GetComponentInParent<CharController>();
    }

    void Update(){
        //Solve();
    }
    
    public void Solve(){
        if(_keyObjects.Count != 0){
            if(_solved == false){
                foreach(var item in _keyObjects){
                    if(item._rightGuess == true){
                        _rightGuessed++;
                    }
                }
                if(_rightGuessed == _keyObjects.Count){
                    StartCoroutine(GoodAnswer());
                }else{
                    BadAnswer();
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
                    StartCoroutine(GoodAnswer());
                }else{
                    BadAnswer();
                }
            }
        }
    }

    IEnumerator GoodAnswer(){
        yield return new WaitForSeconds(.5f);
        _goal.Solved();
        _solved = true;
        _examinate.DeselectingMesh();
        /*if(_keyObjects[0]._keyObject != TypeOfKeyObject.Receptacle){
            StartCoroutine(_char.GoBackToReality());
        }*/
        _examinate._boxCollider.enabled = false;
        foreach(var item in _keyObjects){
            if(item._keyObject == TypeOfKeyObject.Receptacle){
                foreach (var keyObject in item._pickUps)
                {
                    keyObject.GetComponentInChildren<MeshCollider>().enabled = false;
                    keyObject.GetComponentInChildren<Rigidbody>().Sleep();
                }
            }else{
                item._solved = true;
            }
        }
    }

    void BadAnswer(){
        _rightGuessed = 0;
    }
}
