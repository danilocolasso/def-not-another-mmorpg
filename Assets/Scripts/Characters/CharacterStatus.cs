using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatus", menuName = "Scriptable Objects/Character/Status")]
public class CharacterStatus: ScriptableObject
{
    [Range(0, 10)] public float moveSpeed = 5f;
    [Range(0, 10)] public float attackRange = 1f;

    public float MoveSpeed => moveSpeed;
    public float AttackRange => attackRange;
}
