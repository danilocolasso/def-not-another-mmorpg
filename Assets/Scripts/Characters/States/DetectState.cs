using System.Collections;
using UnityEngine;

public class DetectState : IState
{
    private readonly Character target;

    public DetectState(Character target)
    {
        this.target = target;
    }

    public void OnStateEnter(Character character)
    {   
        character.StartCoroutine(EnterBattleCoroutine(character));
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

    private IEnumerator EnterBattleCoroutine(Character character)
    {
        yield return new WaitForSeconds(2f);
        ICommand command = new EnterBattleCommand(character, target);
        command.Execute();
    }
}
