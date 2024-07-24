public class InBattleState : IState
{
    private readonly Character target;

    public InBattleState(Character target)
    {
        this.target = target;
    }

    public void OnStateEnter(Character character)
    {
        if (character.Target == null)
        {
            character.SetTarget(target);
        }

        // Set weapon renderers active
        // weapon an abstract ScriptableObject that contains the sprites for the weapon, it extends Item
        // fist, shield, sword, axe, mace, dagger, spear, staff, wand, bow, gun
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