using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Homework : MonoBehaviour, IBiteable
{
    [Header("TargetRotate")]
    public Transform targetR;

    [Header("Snap")]
    public bool isStuck = false;
    public bool canDrag = true;

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (isStuck) return;

    //    if (collision.gameObject.CompareTag(this.tag))
    //    {
    //        targetR = collision.transform;
    //        Snapping(collision.transform);
    //        Debug.Log("snap");
    //    }
    //}

    private void Start()
    {
        BehaviorController.instance.AddBiteable(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isStuck) return;

        if (collision.gameObject.CompareTag(this.tag))
        {
            targetR = collision.transform;
            Snapping(collision.transform);
            Debug.Log("snap");
        }
    }

    void Snapping(Transform target)
    {
        isStuck = true;
        canDrag = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Collider2D col = GetComponent<Collider2D>();

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.freezeRotation = true;

        col.enabled = false;

        // snapping animation
        LeanTween.move(gameObject, target.position, 0.3f).setEase(LeanTweenType.easeOutQuad).setOnComplete(() => { Debug.Log("snapAnimation"); });
        StartCoroutine(SetParentAfterDelay(target, 0.3f));

        //no rotate
        transform.rotation = targetR.rotation;

        PuzzleManager.Instance.PieceSnapped();

    }


    //mouse drag
    void OnMouseDrag()
    {
        if (!isStuck && canDrag)
        {
            transform.position = GetMousePos();
        }
    }
    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    IEnumerator SetParentAfterDelay(Transform target, float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.SetParent(target);
    }

    public void GotBite()
    {
        if (isStuck && !canDrag)
            PuzzleManager.Instance.PieceUnSnapped();
    }

    public void ThrowAway()
    {
        transform.parent = null;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Collider2D col = GetComponent<Collider2D>();

        LayerMask mask = LayerMask.GetMask("puzzle");
        col.excludeLayers |= (1 << mask);

        rb.isKinematic = false;
        rb.freezeRotation = false;
        Vector2 force = new Vector2(Random.Range(0f, 400f), Random.Range(0f, 400f));
        rb.AddForce(force);

        LeanTween.delayedCall(gameObject, .1f, () =>
        {
            col.excludeLayers &= ~(1 << mask);
            col.enabled = true;
        });
        LeanTween.delayedCall(gameObject, .5f, () =>
        {
            isStuck = false;
            canDrag = true;
        });
    }
}
