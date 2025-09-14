// Finite-State Machine base on post:
// https://www.veryenjoy.tw/enjoy/article/160

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BehaviorController : MonoBehaviour
{
    [Header("Moving Settings")]
    [SerializeField] float gotTargetDistance;
    [SerializeField] float nextMoveTime;
    [SerializeField] float nextMoveRandomThreshold;

    [Header("Eating Settings")]
    [SerializeField] float eatingDuration;

    [Header("Throwing Settings")]
    [SerializeField] float throwingDuration;

    private float _speed;

    GameObject chaseTarget;
    Animator animator;
    Queue<GameObject> foodsQueue;
    //Queue<PetBehavior> behaviorsQueue;
    List<IBiteable> biteables;

    #region Init
    public void Init(float speed)
    {
        _speed = speed;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        foodsQueue = new Queue<GameObject>();
        //behaviorsQueue = new Queue<PetBehavior>();
        biteables = new List<IBiteable>();
    }

    private void Start()
    {
        biteables = FindObjectsOfType<MonoBehaviour>().OfType<IBiteable>().ToList();
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
        //if (currentBehavior != null & !currentBehavior.CanTransition())
        //{
        //    behaviorsQueue.Enqueue(newBehavior);
        //    return;
        //}

        //if (behaviorsQueue.Count > 0)
        //{

        //}

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
        if (chaseTarget == null)
        {
            Debug.LogWarning("Chasing target is not defined.");
            return;
        }

        // Moving forward to the target
        transform.position += _speed * Time.deltaTime * (chaseTarget.transform.position - transform.position).normalized;
        // Check if target is near enough
        if (Vector2.Distance(transform.position, chaseTarget.transform.position) < gotTargetDistance)
        {
            // TODO: Bite!
            Debug.Log($"Pet catched {chaseTarget.name}!");
            ChangeBehavior(new BiteBehavior(), false);
        }
    }

    public void PetFacingWhenChasing()
    {
        FlipCheck(chaseTarget.transform.position.x);
    }

    public void WalkToPosition()
    {
        var _x = Random.Range(-8f, 8f);
        var _y = Random.Range(-4f, 4f);
        var _pos = new Vector2(_x, _y);

        var distance = Vector2.Distance(transform.position, _pos);
        var duration = distance / _speed;

        FlipCheck(_x);
        LeanTween.move(gameObject, _pos, duration).setOnComplete(() => ChangeBehavior(new IdleBehavior()));
    }

    private void FlipCheck(float _x)
    {
        float toY = 0f;

        if (_x > transform.position.x) // Need flip Y axis
            toY = 180f;
        else
            toY = 0f;

        LeanTween.rotateY(gameObject, toY, .3f);
    }

    public void Bite()
    {
        LeanTween.cancel(gameObject);

        var biteable = chaseTarget.GetComponent<IBiteable>();
        if (biteable == null)
        {
            Debug.LogWarning($"Chasing item \"{chaseTarget.name}\" can't bite!");
            ChangeBehavior(new IdleBehavior());
            return;
        }

        float delayDuration = throwingDuration;
        PetBehavior nextBehavior = new ThrowBehavior();

        if (chaseTarget.CompareTag("food"))
        {
            PlayAnimation("Eating");
            delayDuration = eatingDuration;
        }
        else
        {
            PlayAnimation("Bite");
        }

        biteable.GotBite();
        Debug.Log($"{chaseTarget.name} got bite!");

        if (foodsQueue.Count > 0)
        {
            chaseTarget = foodsQueue.Dequeue();
            nextBehavior = new ChaseBehavior();
        }

        LeanTween.delayedCall(delayDuration, () => { ChangeBehavior(nextBehavior); });
    }

    public void Throw()
    {
        if (chaseTarget == null) return;

        chaseTarget.GetComponent<IBiteable>().ThrowAway();
        chaseTarget = null;
        LeanTween.delayedCall(1f, () => ChangeBehavior(new IdleBehavior()));
    }

    public void RandomNextMove()
    {
        if (chaseTarget != null && chaseTarget.CompareTag("food")) ChangeBehavior(new ChaseBehavior());

        var nextMoveIndex = Random.Range(0, 4);
        var delayTime = Random.Range(nextMoveTime - nextMoveRandomThreshold, nextMoveTime + nextMoveRandomThreshold);
        PetBehavior nextBehavior = new IdleBehavior();

        switch ((PetState)nextMoveIndex)
        {
            case PetState.Walk:
                nextBehavior = new WalkBehavior();
                break;
            //case PetState.Chase:
            default:
                nextBehavior = ChaseRandomTarget();
                break;
        }

        LeanTween.delayedCall(delayTime, () => ChangeBehavior(nextBehavior));
    }

    private PetBehavior ChaseRandomTarget()
    {
        if (biteables.Count == 0) return new IdleBehavior();

        var randTargetIndex = Random.Range(0, biteables.Count);
        chaseTarget = biteables[randTargetIndex].gameObject;

        return new ChaseBehavior();
    }

    public void FoodGenerated(GameObject food)
    {
        foodsQueue.Enqueue(food);

        if (chaseTarget == null || !chaseTarget.CompareTag("food"))
        {
            chaseTarget = foodsQueue.Dequeue();
            ChangeBehavior(new ChaseBehavior());
        }
    }

    public void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }

    #endregion
}
