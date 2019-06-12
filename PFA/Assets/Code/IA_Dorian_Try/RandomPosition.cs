using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    public readonly static int EXIT_INDEX_VALUE = 5; // constante chelou tqt


    [SerializeField] Vector3[] _randomPosition;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Ghost")
        {
            Debug.Log("OnTriggerEnter");
            transform.position = GenerateRandomPosition();
        }
    }

    // au cas où wola
    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Ghost")
        {
            Debug.Log("OnTriggerStay");
            transform.position = GenerateRandomPosition();
        }
    }

    Vector3 GenerateRandomPosition()
    {
        int exitIndex = 0;
        int randomNumber = 0;

        do
        {
            exitIndex++;
            randomNumber = Random.Range(0, _randomPosition.Length);
        }
        while (_randomPosition[randomNumber] == transform.position && exitIndex < EXIT_INDEX_VALUE);

        return _randomPosition[randomNumber];
    }

    private void OnDrawGizmos()
    {
        foreach (var item in _randomPosition)
        {
            Gizmos.DrawWireSphere(item, 1);
        }
    }
}
