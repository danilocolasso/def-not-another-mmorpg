using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class AbstractCharacterGraphics : MonoBehaviour
{
    private int isMovingHash;
    private int enterBattleHash;
    private int exitBattleHash;
    private int moveSpeedHash;
    protected Animator animator;
    protected Character character;

    public abstract void SetColor(Color32 color);

    public virtual void Initialize(Character character)
    {
        this.character = character;
        SetColor(character.Data.Graphics.SkinColor);
    }

    public virtual void EnterBattle(Character target)
    {
        animator.SetTrigger(enterBattleHash);
    }

    public virtual void ExitBattle()
    {
        animator.SetTrigger(exitBattleHash);
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

    public virtual void Flip(bool flip)
    {
        float y = flip ? transform.rotation.y + 180 : transform.rotation.y - 180;
        transform.Rotate(0, y, 0);
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
        moveSpeedHash = Animator.StringToHash("MoveSpeed");
        enterBattleHash = Animator.StringToHash("EnterBattle");
        exitBattleHash = Animator.StringToHash("ExitBattle");
    }
}
