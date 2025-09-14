public class ThrowBehavior : PetBehavior
{
    public override PetState State { get {  return PetState.Throw; } }

    protected override void OnEnter()
    {
        base.OnEnter();
        behaviorController.Throw();
    }
}
