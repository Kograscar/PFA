using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        Debug.Log("Trigger");
        if(other.CompareTag("Token")){
            Debug.Log("Hit");
            other.GetComponentInChildren<Token>().YouVeBeenPranked();
        }
    }
}
