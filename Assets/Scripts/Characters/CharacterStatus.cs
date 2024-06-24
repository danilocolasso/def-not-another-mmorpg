using UnityEngine;

[System.Serializable]
public class CharacterStatus
{
    [Range(0, 10)] public float moveSpeed = 5f;
    [Range(0, 10)] public float attackRange = 1f;

    public float MoveSpeed => moveSpeed;
    public float AttackRange => attackRange;
}
