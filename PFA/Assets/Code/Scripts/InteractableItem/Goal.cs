using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class Goal : MonoBehaviour
{
    public GoalType _goal;
    [Header("Instatiate Var")]
    [SerializeField] Transform _instantiatePoint;
    [SerializeField] GameObject _instantiateObject;
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
            case GoalType.Instantiate:
                    Instantiate(_instantiateObject, _instantiatePoint.position, _instantiatePoint.rotation);
                break;
            case GoalType.MileStone:
                    GameObject.FindObjectOfType<ShowVictoryScreen>()._milestones++;
                break;
        }
    }
}
