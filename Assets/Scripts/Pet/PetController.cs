using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    [SerializeField] BehaviorController behaviorController;
    [SerializeField] float speed;
    [SerializeField] GameObject testTarget;

    private void Awake()
    {
        behaviorController = GetComponent<BehaviorController>();
        behaviorController.Init(speed);
    }

    private void Start()
    {
        behaviorController.SetChasingTarget(testTarget);
        behaviorController.ChangeBehavior(new ChaseBehavior());
    }
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