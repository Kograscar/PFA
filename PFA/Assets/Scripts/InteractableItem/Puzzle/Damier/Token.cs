using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    Damier _damier;

    void Start(){
        _damier = GetComponentInParent<Damier>();
    }

    public void YouVeBeenPranked(){
        _damier.ResetToken();
    }
}