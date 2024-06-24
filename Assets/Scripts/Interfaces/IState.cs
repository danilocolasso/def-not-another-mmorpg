public interface IState
{
    public void OnStateEnter(Character character);
    public void OnStateExit(Character character);
    public void OnStateFixedUpdate(Character character);
    public void OnStateUpdate(Character character);
}
