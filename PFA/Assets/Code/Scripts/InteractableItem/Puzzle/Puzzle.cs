using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Puzzle : InteractableItem
{
    [SerializeField] List<KeyObject> _keyObjects;
    int _rightGuessed;
    [SerializeField] public bool _solved = false;
    [SerializeField] Goal _goal;
    Examinate _examinate;
    CharController _char;


    protected override void Awake(){
        
    }

    void Start(){
        _examinate = GetComponentInChildren<Examinate>();
        _char = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CharController>();
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
        if(_examinate != null){
            _examinate.DeselectingMesh();
            _examinate._boxCollider.enabled = false;
        }
        if(_keyObjects[0]._keyObject == TypeOfKeyObject.Receptacle){
            foreach(var item in _keyObjects){
                item._solved = true;
                foreach (var keyObject in item._pickUps)
                {
                    MeshCollider meshcollider = keyObject.GetComponentInChildren<MeshCollider>();
                    if(meshcollider == null){
                        meshcollider.enabled = false;
                    }else{
                        keyObject.GetComponentInChildren<BoxCollider>().enabled = false;
                    }
                    keyObject.GetComponentInChildren<Rigidbody>().Sleep();
                }
            }
        }else if(_keyObjects[0]._keyObject == TypeOfKeyObject.TruDat){
            MeshCollider[] meshcolliders = GetComponentsInChildren<MeshCollider>();
            foreach (var item in meshcolliders)
            {
                item.enabled = false;
            }
            BoxCollider[] boxcolliders = GetComponentsInChildren<BoxCollider>();
            foreach (var item in boxcolliders)
            {
                item.enabled = false;
            }
        }
    }

    void BadAnswer(){
        _rightGuessed = 0;
    }
}
