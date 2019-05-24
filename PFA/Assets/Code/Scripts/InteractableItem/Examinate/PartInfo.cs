using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartInfo : MonoBehaviour
{
    [SerializeField] Part _part;
    public int _xTransform;
    public int _yTransform;
    public Space _transformSpace;
    public RotOrPos _transformType;
    public GameObject _gameObject;

    void Start()
    {
        _xTransform = _part.XTransform;
        _yTransform = _part.YTransform;
        _transformSpace = _part.TransformSpace;
        _transformType = _part._transformType;
    }
}
