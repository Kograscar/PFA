using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVictoryScreen : MonoBehaviour
{
    public int _milestones;
    [SerializeField] GameObject _victoryScreen;


    void Update()
    {
        if(_milestones >= 2){
            _victoryScreen.SetActive(true);
            GameObject.FindObjectOfType<CharController>().gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
