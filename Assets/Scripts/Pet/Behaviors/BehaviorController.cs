// Finite-State Machine base on post:
// https://www.veryenjoy.tw/enjoy/article/160

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorController : MonoBehaviour
{
    [SerializeField] float gotTargetDistance;

    private float _speed;

    GameObject chaseTarget;
    Animator animator;

    #region Init
    public void Init(float speed)
    {
        _speed = speed;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    #endregion

    #region FSM Workflow
    private PetBehavior currentBehavior;

    private void Update()
    {
        currentBehavior?.OnBehaviorUpdate();
    }

    public bool IsCurrentBehavior<BehaviorType>() where BehaviorType : PetBehavior
    {
        return currentBehavior is BehaviorType;
    }

    public void ChangeBehavior(PetBehavior newBehavior, bool playAnimation = true)
    {
        currentBehavior?.OnBehaviorExit();
        currentBehavior = newBehavior;
        currentBehavior.OnBehaviorEnter(this);
        if (playAnimation) PlayAnimation(currentBehavior.State.ToString());
    }
    #endregion

    #region Behavior
    public void SetChasingTarget(GameObject target)
    {
        chaseTarget = target;
    }

    public void ChasingTarget()
    {
        if (chaseTarget ==  null)
        {
            Debug.LogWarning("Chasing target is not defined.");
            return;
        }

        // Moving forward to the target
        transform.position += (chaseTarget.transform.position - transform.position).normalized * Time.deltaTime;
        // Check if target is near enough
        if (Vector2.Distance(transform.position, chaseTarget.transform.position) < gotTargetDistance)
        {
            // TODO: Bite!
            Debug.Log($"Pet catched {chaseTarget.name}!");
            ChangeBehavior(new BiteBehavior());
        }
    }


    public void Bite()
    {
        var biteable = chaseTarget.GetComponent<IBiteable>();
        if (biteable == null)
        {
            Debug.LogWarning($"Chasing item \"{chaseTarget.name}\" can't bite!");
            ChangeBehavior(new IdleBehavior());
            return;
        }
        biteable.GotBite();
        LeanTween.delayedCall(1f, () => { ChangeBehavior(new ThrowBehavior()); });
    }

    public void Throw()
    {
        chaseTarget.GetComponent<IBiteable>().ThrowAway();
        chaseTarget = null;
        ChangeBehavior(new IdleBehavior());
    }

    public void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }

    #endregion
}
