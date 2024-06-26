using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private Character character;
    private BattleList battleList;

    public bool IsInBattle => battleList.Count > 0;

    private void Awake()
    {
        character = GetComponent<Character>();
        battleList = new BattleList(character);
    }

    public void Attack(Character target)
    {
        if (target.IsDead)
        {
            Debug.Log("[BattleManager] " + target.name + " --> Already dead");
            return;
        }

        if (!InRange(target))
        {
            Debug.Log("[BattleManager] " + character.name + " --> Out of range --> " + target.name);
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

    public void Die()
    {
        battleList.Clear();
    }

    private bool InRange(Character target)
    {
        return Vector2.Distance(character.transform.position, target.transform.position)
            <= character.StatusManager.AttackRange.Value;
    }
}
