using System;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Rendering;

public class HumanoidGraphics : CharacterGraphics
{
    [Serializable]
    public struct Equipable
    {
        public SortingGroup Group;
        public SpriteRenderer Skin;
        public SpriteRenderer Equipment;
        [HideInInspector] public int DefaultSortingOrder;

        public readonly void Show()
        {
            Group.gameObject.SetActive(true);
        }

        public readonly void Hide()
        {
            Group.gameObject.SetActive(false);
        }

        public readonly void Flip(bool flip)
        {
            Group.transform.Rotate(0, flip ? FLIP_ANGLE : -FLIP_ANGLE, 0);
        }
    }

    [Serializable]
    public struct Arm
    {
        public Equipable Hand;
        public SpriteRenderer Weapon;

        public readonly void Reset()
        {
            Hand.Group.transform.localRotation = Quaternion.identity;
            Hand.Group.sortingOrder = Hand.DefaultSortingOrder;
            Hand.Show();
        }
    }

    [Serializable]
    public struct Arms_
    {
        public Arm Right;
        public Arm Left;
    }

    [Serializable]
    public struct Legs_
    {
        public Equipable Right;
        public Equipable Left;
    }

    [SerializeField] private Equipable head;
    [SerializeField] private Equipable chest;
    [SerializeField] private Arms_ arms;
    [SerializeField] private Equipable underware;
    [SerializeField] private Legs_ legs;

    public Equipable Head => head;
    public Equipable Chest => chest;
    public Arms_ Arms => arms;
    public Equipable Underware => underware;
    public Legs_ Legs => legs;

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
        arms.Right.Hand.DefaultSortingOrder = arms.Right.Hand.Group.sortingOrder;
        arms.Left.Hand.DefaultSortingOrder = arms.Left.Hand.Group.sortingOrder;
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
        bool isFacingRight = (IsFlipped && isHeadFlipped) || (!IsFlipped && !isHeadFlipped);
        bool shouldFaceRight = direction.x > 0;
        bool shouldFlipHead = isFacingRight != shouldFaceRight;

        if (shouldFlipHead)
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
        arms.Right.Hand.Flip(flip);
        arms.Left.Hand.Flip(flip);

        if (character.IsOutOfBattle)
        {
            arms.Left.Hand.Flip(flip);
            arms.Left.Hand.Flip(flip);
            arms.Right.Hand.Flip(flip);
        }

        (arms.Right.Hand.Group.sortingOrder, arms.Left.Hand.Group.sortingOrder) = (arms.Left.Hand.Group.sortingOrder, arms.Right.Hand.Group.sortingOrder);
    }
}