using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homework : MonoBehaviour
{
    [Header("Snap")]
    public bool isStuck = false;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(this.tag))
        {
            Snapping(collision.transform);
            Debug.Log("snap");
        }
    }
    void Snapping(Transform target)
    {
        isStuck = true;
        transform.position = target.position;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
        transform.SetParent(target);
    }

    //·Æ¹«©ì¦²
    void OnMouseDrag()
    {
        transform.position = getMousePos();
    }
    Vector3 getMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
