using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private Character character;
    private BattleList battleList;

    public bool IsInBattle => battleList.Count > 0;

    public void Initialize(Character character)
    {
        this.character = character;
        battleList = new BattleList(character);
    }

    public void Attack(Character target)
    {
        if (target.IsDead)
        {
            return;
        }

        if (!IsInRange(target))
        {
            return;
        }

        ICommand attack = new AttackCommand(character, target);
        attack.Execute();

        if (target.IsAlive)
        {
            EnterBattle(target);
        }
    }

    public void EnterBattle(Character target)
    {
        if (battleList.Add(target))
        {
            ICommand enterBattle = new EnterBattleCommand(character, target);
            enterBattle.Execute();
        }
    }

    public void ExitBattle(Character target)
    {
        if (battleList.Remove(target))
        {
            ICommand exitBattle = new ExitBattleCommand(character, target);
            exitBattle.Execute();
        }
    }

    public bool IsInBattleWith(Character target)
    {
        return battleList.Contains(target);
    }

    public void Die()
    {
        battleList.Clear();
    }

    private bool IsInRange(Character target)
    {
        return Vector2.Distance(character.transform.position, target.transform.position)
            <= character.Status.AttackRange;
    }
}
