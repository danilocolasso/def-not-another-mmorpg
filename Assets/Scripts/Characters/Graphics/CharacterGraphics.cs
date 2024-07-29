using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class CharacterGraphics : MonoBehaviour
{
    protected const int FLIP_ANGLE = 180;

    private int isMovingHash;
    private int enterBattleHash;
    private int exitBattleHash;
    private int moveSpeedHash;
    private int hasTargetHash;
    protected Animator animator;
    protected Character character;
    private bool flipped = false;

    public bool IsFlipped => transform.rotation.y != 0;

    protected abstract void SetColor(Color32 color);
    protected abstract void SetSprites();
    public abstract void Equip(Weapon weapon, Weapon.Hand hand);
    public abstract void Unequip(Weapon.Hand hand);
    public abstract void Aim(Character target);

    public virtual void Initialize(Character character)
    {
        animator = GetComponent<Animator>();
        this.character = character;
        SetSprites();
        SetColor(character.Data.Graphics.SkinColor);
    }

    public virtual void SetTarget(Character target)
    {
        animator.SetBool(hasTargetHash, target != null);
        Debug.Log($"{character} has target: {target != null}");
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

    public virtual void SetDirection(Vector2 direction)
    {
        if (direction.x < 0 && !flipped)
        {
            Flip(true);
            flipped = true;
        }
        else if (direction.x > 0 && flipped)
        {
            Flip(false);
            flipped = false;
        }
    }

    public virtual void Die()
    {
        SetMoving(Vector2.zero);
    }

    public virtual void Flip(bool flip)
    {
        transform.Rotate(0, flip ? FLIP_ANGLE : -FLIP_ANGLE, 0);
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
        hasTargetHash = Animator.StringToHash("HasTarget");
    }
}
