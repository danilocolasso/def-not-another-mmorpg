public abstract class Equipment : Item
{
    public override void Use(Character character)
    {
        character.Equip(this);
    }
}
