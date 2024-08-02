using UnityEngine;

public class HumanoidGraphics : CharacterGraphics
{
    [SerializeField] private HumanoidHandsGraphics handsGraphics;

    [SerializeField] private Equipable head;
    [SerializeField] private Equipable chest;
    [SerializeField] private HumanoidArms arms;
    [SerializeField] private Equipable underware;
    [SerializeField] private HumanoidLegs legs;

    public HumanoidArms Arms => arms;

    public override void Initialize(Character character)
    {
        base.Initialize(character);

        if (handsGraphics == null)
        {
            Debug.LogWarning($"Performance --> {character} has no HumanoidHandsGraphics assigned in the Inspector!");
            handsGraphics = GetComponentInChildren<HumanoidHandsGraphics>();
        }

        handsGraphics.Initialize();
    }

    public override void SetMoving(Vector2 direction, float speed)
    {
        base.SetMoving(direction, speed);
        handsGraphics.SetMoving(direction, speed);
    }

    public override void SetTarget(Character target)
    {
        base.SetTarget(target);
        handsGraphics.SetTarget(target);

        if (character.IsOutOfBattle)
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

    public void Wield()
    {
        handsGraphics.Wield();
    }

    public void Unwield()
    {
        handsGraphics.Unwield();
    }

    public override void EnterBattle(Character target)
    {
        base.EnterBattle(target);
        handsGraphics.EnterBattle();
    }

    public override void ExitBattle()
    {
        base.ExitBattle();
        handsGraphics.ExitBattle();
    }

    public override void Flip(bool flip)
    {
        base.Flip(flip);
        SwapHands(flip);
        FaceTarget();
    }

    private void FixedUpdate()
    {
        FaceTarget();
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

        bool isHeadFlipped = head.Group.transform.localRotation.y != 0;
        bool isFacingRight = IsFlipped ? isHeadFlipped : !isHeadFlipped;
        bool shouldFaceRight = direction.x > 0;

        if (isFacingRight != shouldFaceRight)
        {
            head.Flip(!isHeadFlipped);
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
        
        SwapWeapons(flip);
    }

    private void SwapWeapons(bool flip)
    {
        arms.Right.Hand.Weapon.sortingOrder = flip ? arms.Right.Hand.Equipment.sortingOrder + 1 : arms.Right.Hand.Skin.sortingOrder - 1;
        arms.Left.Hand.Weapon.sortingOrder = flip ? arms.Left.Hand.Equipment.sortingOrder + 1 : arms.Left.Hand.Skin.sortingOrder - 1;
    }
}