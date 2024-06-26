public class EnterBattleCommand : ICommand
{
    private readonly Character character;
    private readonly Character target;

    public EnterBattleCommand(Character character, Character target)
    {
        this.character = character;
        this.target = target;
    }

    public void Execute()
    {
        character.SetState(new InBattleState(target));
        character.EnterBattle(target);

        target.SetState(new InBattleState(character));
        target.EnterBattle(character);
    }
}
