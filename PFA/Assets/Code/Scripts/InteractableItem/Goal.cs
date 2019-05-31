using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class Goal : MonoBehaviour
{
    enum GoalType{Light, Anim, Timeline};
    [SerializeField] GoalType _goal;
    public void Solved(){
        switch(_goal){
            case GoalType.Light:
                    GetComponent<Light>().color = Color.green;
                break;

            case GoalType.Anim:
                    GetComponent<Animator>().SetTrigger("Anim");
                break;
            case GoalType.Timeline:
                    GetComponent<PlayableDirector>().Play();
                break;
        }
    }
}
