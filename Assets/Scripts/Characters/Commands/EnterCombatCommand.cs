using UnityEngine;

public class EnterCombatCommand : ICommand
{
    private readonly Character character;
    private readonly Character target;

    public EnterCombatCommand(Character character, Character target)
    {
        this.character = character;
        this.target = target;
    }

    public void Execute()
    {
        character.SetState(new InCombatState(target));
        target.SetState(new InCombatState(character));
    }
}
