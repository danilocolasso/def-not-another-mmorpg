using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class AbstractCharacterGraphics : MonoBehaviour
{
    private int isMovingHash;
    private int isInBattleHash;
    private int moveSpeedHash;
    protected Animator animator;

    public abstract void SetColor(Color32 color);

    public virtual void Initialize(Character character)
    {
        SetColor(character.Data.Graphics.SkinColor);
    }

    public virtual void EnterBattle(Character target)
    {
        animator.SetBool(isInBattleHash, true);
    }

    public virtual void ExitBattle()
    {
        animator.SetBool(isInBattleHash, false);
    }

    public virtual void SetInBattle(bool isInBattle)
    {
        animator.SetBool(isInBattleHash, isInBattle);
    }
    
    public virtual void SetMoving(Vector2 direction, float speed = 1)
    {
        animator.SetBool(isMovingHash, direction != Vector2.zero);
        animator.SetFloat(moveSpeedHash, speed);
    }

    public virtual void SetDead()
    {
        SetMoving(Vector2.zero);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetHashes();
    }

    private void SetHashes()
    {
        isMovingHash = Animator.StringToHash("IsMoving");
        isInBattleHash = Animator.StringToHash("IsInBattle");
        moveSpeedHash = Animator.StringToHash("MoveSpeed");
    }
}
