
public class ChaseBehavior : PetBehavior
{
    public override PetState State { get {  return PetState.Chase; } }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        behaviorController.ChasingTarget();
    }
}
