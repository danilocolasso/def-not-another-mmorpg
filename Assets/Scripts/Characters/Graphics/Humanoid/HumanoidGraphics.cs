using UnityEngine;

public class HumanoidGraphics : CharacterGraphics
{
    [SerializeField] private Equipable head;
    [SerializeField] private Equipable chest;
    [SerializeField] private HumanoidArms arms;
    [SerializeField] private Equipable underware;
    [SerializeField] private HumanoidLegs legs;

    public Equipable Head => head;
    public Equipable Chest => chest;
    public HumanoidArms Arms => arms;
    public Equipable Underware => underware;
    public HumanoidLegs Legs => legs;

    public override void SetTarget(Character target)
    {
        base.SetTarget(target);

        if (!character.IsInBattle)
        {
            return;
        }

        if (target == null)
        {
            ExitBattle();
        }
        else
        {
            EnterBattle(target);
        }
    }

    public override void Flip(bool flip)
    {
        base.Flip(flip);
        SwapHands(flip);
        FaceTarget();
    }

    private void Awake()
    {
        SetDefaultSortingOrders();
    }

    private void FixedUpdate()
    {
        FaceTarget();
    }

    private void SetDefaultSortingOrders()
    {
        arms.Right.DefaultSorting = arms.Right.Group.sortingOrder;
        arms.Left.DefaultSorting = arms.Left.Group.sortingOrder;
    }

    protected override void SetSkinSprites()
    {
        arms.Right.Hand.Skin.sprite = character.Data.Graphics.Hand;
        arms.Left.Hand.Skin.sprite = character.Data.Graphics.Hand;
        head.Skin.sprite = character.Data.Graphics.Head;
        chest.Skin.sprite = character.Data.Graphics.Chest;
        underware.Skin.sprite = character.Data.Graphics.Underware;
        legs.Right.Skin.sprite = character.Data.Graphics.Leg;
        legs.Left.Skin.sprite = character.Data.Graphics.Leg;
    }

    protected override void SetSkinColor(Color32 color)
    {
        head.Skin.color = color;
        arms.Right.Hand.Skin.color = color;
        arms.Left.Hand.Skin.color = color;
        chest.Skin.color = color;
        legs.Right.Skin.color = color;
        underware.Skin.color = color;
        legs.Left.Skin.color = color;
        arms.Left.Hand.Skin.color = color;
    }

    private void FaceTarget()
    {
        if (character.Target == null)
        {
            ResetHead();
            return;
        }

        Vector3 direction = (character.Target.transform.position - transform.position).normalized;

        bool isHeadFlipped = Head.Group.transform.localRotation.y != 0;
        bool isFacingRight = IsFlipped ? isHeadFlipped : !isHeadFlipped;
        bool shouldFaceRight = direction.x > 0;

        if (isFacingRight != shouldFaceRight)
        {
            Head.Flip(!isHeadFlipped);
        }
    }

    private void ResetHead()
    {
        if (head.Group.transform.localRotation.y != 0)
        {
            head.Flip(false);
        }
    }

    private void SwapHands(bool flip)
    {
        (arms.Right.Group.sortingOrder, arms.Left.Group.sortingOrder) = (arms.Left.Group.sortingOrder, arms.Right.Group.sortingOrder);

        arms.Right.Flip(flip);
        arms.Left.Flip(flip);

        arms.Right.Hand.Flip(flip);
        arms.Left.Hand.Flip(flip);
    }
}