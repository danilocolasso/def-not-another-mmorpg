using UnityEngine;

public class AttackCommand : ICommand
{
    private readonly Character character;
    private readonly Character target;

    public AttackCommand(Character character, Character target)
    {
        this.character = character;
        this.target = target;
    }

    public void Execute()
    {
        target.Hit((int)character.StatusManager.AttackDamage.Value);
    }

}
