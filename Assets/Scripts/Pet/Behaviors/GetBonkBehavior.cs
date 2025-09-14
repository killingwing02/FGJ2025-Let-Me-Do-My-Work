public class GetBonkBehavior : PetBehavior
{
    public override PetState State { get { return PetState.GetBonk; } }

    protected override void OnEnter()
    {
        base.OnEnter();

        LeanTween.delayedCall(behaviorController.gameObject, 2f, () => behaviorController.PlayAnimation("Angry"));
        LeanTween.delayedCall(behaviorController.gameObject, 5f, () => behaviorController.ChangeBehavior(new IdleBehavior()));
    }
}
