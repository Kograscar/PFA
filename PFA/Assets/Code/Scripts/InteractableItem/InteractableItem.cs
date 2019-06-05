using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    protected virtual void Awake(){
        gameObject.tag ="InteractableItem";
    } 

    public virtual void Use(GameObject player){
       
    }

    public virtual void UnUse(){

    }
}
