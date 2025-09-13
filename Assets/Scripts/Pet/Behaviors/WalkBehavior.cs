public class WalkBehavior : PetBehavior
{
    public override PetState State { get { return PetState.Walk; } }

    protected override void OnEnter()
    {
        base.OnEnter();

        behaviorController.WalkToPosition();
    }
}
