using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    [SerializeField] BehaviorController behaviorController;
    [SerializeField] float speed;
    [SerializeField] GameObject testTarget;

    #region Singleton
    public static PetController instance;
    private void InitSingleton()
    {
        instance = this;
    }
    #endregion

    private void Awake()
    {
        InitSingleton();

        behaviorController = GetComponent<BehaviorController>();
        behaviorController.Init(speed);
    }

    private void Start()
    {
        // behaviorController.SetChasingTarget(testTarget);
        behaviorController.ChangeBehavior(new SleepBehavior());
        LeanTween.delayedCall(gameObject, 1f, () => behaviorController.ChangeBehavior(new GetBonkBehavior()));
    }

    public void FoodGenerated(GameObject food)
    {
        behaviorController.FoodGenerated(food);
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
    Eating = 4,
    GetBonk = 10,
    Bite = 11,
    Throw = 12,
}