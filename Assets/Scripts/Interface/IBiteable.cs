using UnityEngine;

interface IBiteable
{
    GameObject gameObject { get; }
    void GotBite();
    void ThrowAway();
}
