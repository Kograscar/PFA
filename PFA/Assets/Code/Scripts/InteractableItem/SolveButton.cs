using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolveButton : InteractableItem
{
    Puzzle _puzzle;

    protected override void Start(){
        _puzzle = GetComponentInParent<Puzzle>();
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public override void Use(GameObject player){
        _puzzle.Solve();
        GetComponent<MeshRenderer>().material.color = Color.green;
        StartCoroutine(Clique());
    }

    IEnumerator Clique(){
        yield return new WaitForSeconds(.5f);
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
