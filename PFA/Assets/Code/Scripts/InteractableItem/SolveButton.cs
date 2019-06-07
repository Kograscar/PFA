using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolveButton : InteractableItem
{
    [SerializeField] Puzzle _puzzle;
    bool _solved;
    MeshRenderer _meshRenderer;

    void Start(){
        if(_puzzle == null){
            _puzzle = GetComponentInParent<Puzzle>();
        }
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = Color.red;
    }

    public override void Use(GameObject player){
        if(_solved == false){
            _puzzle.Solve();
            _meshRenderer.material.color = Color.gray;
            StartCoroutine(Clique());
        }
    }

    IEnumerator Clique(){
        yield return new WaitForSeconds(.5f);
        if(_puzzle._solved){
            _meshRenderer.material.color = Color.green;
            _solved = true;
        }else{
            _meshRenderer.material.color = Color.red;
        }
        
    }
}
