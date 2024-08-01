using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HumanoidHandsGraphics : MonoBehaviour
{
    private int isMovingHash;
    private int moveSpeedHash;
    private int hasTargetHash;
    private int wieldHash;
    private int unwieldHash;
    private int aimHash;
    private int attackHash;
    private int enterBattleHash;
    private int exitBattleHash;

    [SerializeField] private Animator animator;

    public void Initialize()
    {
        if (animator == null)
        {
            Debug.LogWarning($"Performance --> {name} has no Animator component attached!");
            animator = GetComponent<Animator>();
        }

        SetHashes();
    }

    public void EnterBattle()
    {
        animator.SetTrigger(enterBattleHash);
        Aim();
    }

    public void ExitBattle()
    {
        animator.SetTrigger(exitBattleHash);
        Aim();
    }

    public void SetTarget(Character target)
    {
        animator.SetBool(hasTargetHash, target != null);
    }

    public void SetMoving(Vector2 direction, float speed = 1)
    {
        animator.SetBool(isMovingHash, direction != Vector2.zero);
        animator.SetFloat(moveSpeedHash, speed);
    }

    public void Wield()
    {
        animator.SetTrigger(wieldHash);
    }

    public void Unwield()
    {
        animator.SetTrigger(unwieldHash);
    }

    public void Aim()
    {
        animator.SetTrigger(aimHash);
    }

    public void Attack()
    {
        animator.SetTrigger(attackHash);
    }

    private void SetHashes()
    {
        isMovingHash = Animator.StringToHash("IsMoving");
        moveSpeedHash = Animator.StringToHash("MoveSpeed");
        hasTargetHash = Animator.StringToHash("HasTarget");
        wieldHash = Animator.StringToHash("Wield");
        unwieldHash = Animator.StringToHash("Unwield");
        aimHash = Animator.StringToHash("Aim");
        attackHash = Animator.StringToHash("Attack");
        enterBattleHash = Animator.StringToHash("EnterBattle");
        exitBattleHash = Animator.StringToHash("ExitBattle");
    }
}
