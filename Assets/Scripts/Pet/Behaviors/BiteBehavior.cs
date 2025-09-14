
public class BiteBehavior : PetBehavior
{
    public override PetState State { get { return PetState.Bite; } }

    protected override void OnEnter()
    {
        base.OnEnter();
        behaviorController.Bite();
    }
}
