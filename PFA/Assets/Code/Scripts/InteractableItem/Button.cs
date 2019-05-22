using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractableItem
{
    Puzzle _puzzle;
    public override void Use(GameObject player){
        _puzzle.Solve();
    }
}
