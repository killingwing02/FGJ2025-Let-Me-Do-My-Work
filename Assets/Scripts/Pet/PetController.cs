using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{

}

public enum PetState
{
    Idle = 0,
    Sleep = 1,
    WalkAround = 2,
    Chase = 3,
    Bite = 11,
    Throw = 12,
}