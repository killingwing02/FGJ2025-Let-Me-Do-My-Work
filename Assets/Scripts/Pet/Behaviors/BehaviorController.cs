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

    #region Init
    public void Init(float speed)
    {
        _speed = speed;
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

    public void ChangeBehavior(PetBehavior newBehavior)
    {
        currentBehavior?.OnBehaviorExit();
        currentBehavior = newBehavior;
        currentBehavior.OnBehaviorEnter(this);
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
        }
    }
    #endregion
}
