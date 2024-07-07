using System.Collections.Generic;

public class BattleList: List<Character>
{
    private readonly Character character;

    public BattleList(Character character)
    {
        this.character = character;
    }

    public new bool Add(Character target)
    {
        if (target.IsAlive && !Contains(target))
        {
            base.Add(target);
            return true;
        }

        return false;
    }

    public new bool Remove(Character target)
    {
        if (Contains(target))
        {
            base.Remove(target);
            return true;
        }
        
        return false;
    }

    public new void Clear()
    {
        for (int i = 0; i < Count; i++)
        {
            this[i].ExitBattle(character);
        }

        base.Clear();
    }
}
