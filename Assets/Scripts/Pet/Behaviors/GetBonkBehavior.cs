public class GetBonkBehavior : PetBehavior
{
    public override PetState State { get { return PetState.GetBonk; } }

    protected override void OnEnter()
    {
        base.OnEnter();

        LeanTween.delayedCall(2f, () => behaviorController.PlayAnimation("Angry"));
        LeanTween.delayedCall(5f, () => behaviorController.ChangeBehavior(new IdleBehavior()));
    }
}
