using UnityEngine;

public class Chase : PetBehavior
{
    public override PetState State { get {  return PetState.Chase; } }

    GameObject target;

    public void BehaviorStart(GameObject obj)
    {
        target = obj;
    }
}
