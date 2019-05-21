using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Token")){
            other.GetComponentInChildren<Token>().YouVeBeenPranked();
        }
    }
}
