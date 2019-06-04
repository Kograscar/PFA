using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    void Awake(){
        gameObject.tag ="InteractableItem";
    }

    protected virtual void Start(){

    }   

    public virtual void Use(GameObject player){
       
    }

    public virtual void UnUse(){

    }
}
