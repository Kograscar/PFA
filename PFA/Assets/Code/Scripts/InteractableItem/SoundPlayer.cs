using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : InteractableItem
{
    public override void Use(GameObject player){
        if(GetComponent<AudioSource>().isPlaying == false){
            GetComponent<AudioSource>().Play();
        }
    }
}
