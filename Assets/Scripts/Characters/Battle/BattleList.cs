using System.Collections.Generic;
using UnityEngine;

public class BattleList: List<Character>
{
    private Character character;

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

    public Character GetNext()
    {
        if (Count == 0)
        {
            return null;
        }

        return this[0];
    }

    public Character GetPrevious(Character target)
    {
        int index = IndexOf(target);
        return index == 0 ? target : this[index - 1];
    }
}
