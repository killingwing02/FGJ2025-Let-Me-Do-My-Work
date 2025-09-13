public class IdleBehavior : PetBehavior
{
    public override PetState State { get { return PetState.Idle; } }

    protected override void OnEnter()
    {
        base.OnEnter();

        behaviorController.RandomNextMove();
    }
}
