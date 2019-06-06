using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DoorKey : InteractableItem
{
    public override void Use(GameObject player){
        GetComponent<PlayableDirector>().Play();
        GameObject.FindObjectOfType<KeyManager>()._keys.Add(this);
    }
}
