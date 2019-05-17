using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Part", menuName = "Datas/PartsRotaion")]
public class Part : ScriptableObject
{
    [SerializeField] [Range(0,1)] int _xRotation;
    public int XRotation
    {
        get{
            return _xRotation;
        }
    }

    [SerializeField] [Range(0,1)] int _yRotation;
    public int YRotation
    {
        get{
            return _yRotation;
        }
    }

    [SerializeField] Space _rotationSpace;
    public Space RotationSpace
    {
        get{
            return _rotationSpace;
        }
    }

    [SerializeField] public RotOrPos _transformType;
    public RotOrPos TransformType
    {
        get{
            return _transformType;
        }
    }
}
