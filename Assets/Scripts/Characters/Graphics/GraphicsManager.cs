using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GraphicsManager : MonoBehaviour
{
    private Animator animator;
    private int isMovingHash;
    private int moveSpeedHash;
    private bool flipped = false;

    [Header("Data")]
    [SerializeField] private CharacterGraphics graphics;

    [Header("Sprite Renderers")]
    [SerializeField] private SpriteRenderer head;
    [SerializeField] private SpriteRenderer rightHand;
    [SerializeField] private SpriteRenderer chest;
    [SerializeField] private SpriteRenderer rightLeg;
    [SerializeField] private SpriteRenderer under;
    [SerializeField] private SpriteRenderer leftLeg;
    [SerializeField] private SpriteRenderer leftHand;

    private void Start()
    {
        SetHashes();
    }

    private void Awake()
    {
        Debug.Assert(graphics != null, $"{GetComponentInParent<Character>()} Graphics is null!");

        animator = GetComponent<Animator>();

        SetSpritesColor();
    }

    public void SetMoving(Vector2 direction, float speed = 1)
    {
        animator.SetBool(isMovingHash, direction != Vector2.zero);
        animator.SetFloat(moveSpeedHash, speed);
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction.x < 0 && !flipped)
        {
            Flip(true);
            return;
        }

        if (direction.x > 0 && flipped)
        {
            Flip(false);
        }
    }

    public void Die()
    {
        SetMoving(Vector2.zero);
    }

    private void Flip(bool flip)
    {
        flipped = flip;
        transform.Rotate(0, flip ? 180 : -180, 0);
    }

    private void SetHashes()
    {
        isMovingHash = Animator.StringToHash("isMoving");
        moveSpeedHash = Animator.StringToHash("moveSpeed");
    }

    private void SetSpritesColor()
    {
        head.color = graphics.SkinColor;
        rightHand.color = graphics.SkinColor;
        chest.color = graphics.SkinColor;
        rightLeg.color = graphics.SkinColor;
        under.color = graphics.SkinColor;
        leftLeg.color = graphics.SkinColor;
        leftHand.color = graphics.SkinColor;
    }
}
