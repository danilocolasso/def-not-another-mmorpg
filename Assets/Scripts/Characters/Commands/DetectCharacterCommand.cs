public class DetectCharacterCommand : ICommand
{
    private readonly Character character;
    private readonly Character target;

    public DetectCharacterCommand(Character character, Character target)
    {
        this.character = character;
        this.target = target;
    }

    public void Execute()
    {
        if (target == null || !target.IsAlive) return;

        if (!character.IsInBattle) {
            character.SetState(new DetectState(target));
            return;
        }

        character.EnterBattle(target);
    }
}
