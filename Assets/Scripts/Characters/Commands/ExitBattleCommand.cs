public class ExitBattleCommand : ICommand
{
    private readonly Character character;
    private readonly Character target;

    public ExitBattleCommand(Character character, Character target)
    {
        this.character = character;
        this.target = target;
    }

    public void Execute()
    {
        character.SetState(new OutOfBattleState());
        character.ExitBattle(target);

        target.SetState(new OutOfBattleState());
        target.ExitBattle(character);
    }
}
