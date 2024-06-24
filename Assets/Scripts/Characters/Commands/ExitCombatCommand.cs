using UnityEngine;

public class ExitCombatCommand : ICommand
{
    private readonly Character character;
    private readonly Character target;

    public ExitCombatCommand(Character character, Character target = null)
    {
        this.character = character;
        this.target = target;
    }

    public void Execute()
    {
        character.SetState(new OutOfCombatState());

        if (target != null) {
            target.SetState(new OutOfCombatState());
        }
    }

}
