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
        // behaviorController.SetChasingTarget(testTarget);
        behaviorController.ChangeBehavior(new SleepBehavior());
        LeanTween.delayedCall(5f, () => behaviorController.ChangeBehavior(new GetBonkBehavior()));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: Check if get hit by puzzle pieces, then change behavior to angery
        //behaviorController.ChangeBehavior(new GetBonkBehavior());
    }
}

public enum PetState
{
    Idle = 0,
    Sleep = 1,
    Walk = 2,
    Chase = 3,
    GetBonk = 10,
    Bite = 11,
    Throw = 12,
}