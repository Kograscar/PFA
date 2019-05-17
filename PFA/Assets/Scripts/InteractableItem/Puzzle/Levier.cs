using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levier : MonoBehaviour
{
    public enum MoveTokenDirection {Horizontal, Vertical};
    MoveTokenDirection _levierMoveDirection;
    Damier _damier;
}
