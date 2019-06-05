using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroyOnLoad : MonoBehaviour
{
    public Button _button1, _button2, _button3;
    void Start()
    {
        
        DontDestroyOnLoad(this);
    }
}
