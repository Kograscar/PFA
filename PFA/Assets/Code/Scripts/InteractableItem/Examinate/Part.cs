using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Part", menuName = "Datas/PartsTransform")]
public class Part : ScriptableObject
{
    [SerializeField] [Range(0,1)] int _xTransform;
    public int XTransform
    {
        get{
            return _xTransform;
        }
    }

    [SerializeField] [Range(0,1)] int _yTransform;
    public int YTransform
    {
        get{
            return _yTransform;
        }
    }

    [SerializeField] Space _transformSpace;
    public Space TransformSpace
    {
        get{
            return _transformSpace;
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
