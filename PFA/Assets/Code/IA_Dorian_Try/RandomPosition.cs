using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
     [SerializeField] Vector3[] _randomPosition;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Ghost")
        {
            print("yes");
            int randomNumber = Random.Range(0, _randomPosition.Length);
            transform.position = _randomPosition[randomNumber];

        }
    }
}
