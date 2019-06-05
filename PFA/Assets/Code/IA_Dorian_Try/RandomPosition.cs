using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    public Vector3[] randomPosition;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Ghost")
        {
            print("yes");
            int randomNumber = Random.Range(0, randomPosition.Length);
            transform.position = randomPosition[randomNumber]; 
        }
    }
}
