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
            Debug.Log("[" + character + "][BattleList] Add to battle list --> [" + target + "] --> [List Count: " + Count + "]");
            base.Add(target);
            return true;
        }

        return false;
    }

    public new bool Remove(Character target)
    {
        if (Contains(target))
        {
            Debug.Log("[" + character + "][BattleList] Remove from battle list --> [" + target + "] --> [List Count: " + Count + "]");
            base.Remove(target);
            return true;
        }
        
        return false;
    }

    public new void Clear()
    {
        for (int i = 0; i < Count; i++)
        {
            this[i].BattleManager.ExitBattle(character);
        }

        base.Clear();
        Debug.Log("[" + character + "][BattleList] Clear battle list --> [List Count: " + Count + "]");
    }

    // public Character GetPrevious()
    // {
        
    // }

    // public Character GetNext()
    // {

    // }

    public void SortByDistance()
    {
        Sort((a, b) => Vector3
            .Distance(a.transform.position, character.transform.position)
            .CompareTo(Vector3.Distance(b.transform.position, character.transform.position)));
    }
}
