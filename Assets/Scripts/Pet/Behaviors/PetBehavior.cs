public abstract class PetBehavior
{
    public abstract PetState State { get; }
    protected BehaviorController behaviorController;

    protected virtual void OnEnter() 
    {
        
    }

    protected virtual void OnUpdate()
    {

    }

    protected virtual void OnExit()
    {

    }

    public virtual bool CanTransition()
    {
        return true;
    }

    public void OnBehaviorEnter(BehaviorController controller)
    {
        this.behaviorController = controller;
        OnEnter();
    }

    public void OnBehaviorExit()
    {
        OnExit();
    }

    public void OnBehaviorUpdate()
    {
        OnUpdate();
    }
}
