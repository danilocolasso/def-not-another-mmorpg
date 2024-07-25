using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class CharacterGraphics : MonoBehaviour
{
    protected const int FLIP_ANGLE = 180;

    private int isMovingHash;
    private int enterBattleHash;
    private int exitBattleHash;
    private int moveSpeedHash;
    private int flippedHash;
    protected Animator animator;
    protected Character character;

    public bool IsFlipped => transform.rotation.y != 0;

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
        transform.Rotate(0, flip ? FLIP_ANGLE : -FLIP_ANGLE, 0);
        animator.SetBool(flippedHash, flip);
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
        flippedHash = Animator.StringToHash("Flipped");
    }
}
