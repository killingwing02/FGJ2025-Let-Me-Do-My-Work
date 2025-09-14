using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IBiteable
{
    public void GotBite()
    {
        Debug.Log("eat");
        Destroy(gameObject);
    }

    public void ThrowAway()
    {
        
    }
}
