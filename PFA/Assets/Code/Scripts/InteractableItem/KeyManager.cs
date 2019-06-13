using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public List<DoorKey> _keys;

    void Start(){
        _keys.Add(new DoorKey());
        _keys.Add(new DoorKey());
    }
}
