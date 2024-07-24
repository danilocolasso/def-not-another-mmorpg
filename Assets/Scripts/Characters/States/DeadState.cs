public class DeadState : IState
{
    public void OnStateEnter(Character character)
    {   
        character.SetTarget(null);
    }

    public void OnStateExit(Character character)
    {
        // Do nothing
    }

    public void OnStateFixedUpdate(Character character)
    {
        // Do nothing
    }

    public void OnStateUpdate(Character character)
    {
        // Do nothing
    }
}
