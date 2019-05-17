using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotOrPos{Rotation, Position};
public class PartInfo : MonoBehaviour
{
    [SerializeField] Part _part;
    public int _xRotation;
    public int _yRotation;
    public Space _rotationSpace;
    public RotOrPos _transformType;

    void Start()
    {
        _xRotation = _part.XRotation;
        _yRotation = _part.YRotation;
        _rotationSpace = _part.RotationSpace;
        _transformType = _part._transformType;
    }
}
