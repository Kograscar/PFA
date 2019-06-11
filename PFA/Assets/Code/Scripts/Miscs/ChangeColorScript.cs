using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorScript : MonoBehaviour
{
    [SerializeField] Color _bleu;
    [SerializeField] Color _violet;
    float _lerpDelay;

    void Update()
    {
        _lerpDelay += Time.deltaTime/5;
        gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(_bleu, _violet, _lerpDelay);
    }
}
