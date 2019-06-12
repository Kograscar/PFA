using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    void Start()
    {
        GameObject.FindObjectOfType<LoadManager>().LoadChoosenScene("Menu");
    }
}
