using System;
using UnityEngine;

public class HumanoidGraphics : CharacterGraphics
{
    [Serializable]
    private struct Body
    {
        public SpriteRenderer RightHand;
        public SpriteRenderer Head;
        public SpriteRenderer Chest;
        public SpriteRenderer Underware;
        public SpriteRenderer RightLeg;
        public SpriteRenderer LeftLeg;
        public SpriteRenderer LeftHand;
    }

    [Serializable]
    private struct Weapons
    {
        public SpriteRenderer Right;
        public SpriteRenderer Left;
    }

    public struct Arm
    {
        public int OriginalSortingOrder;
        public Transform Transform;
        public SpriteRenderer Hand;
        public SpriteRenderer Weapon;
    }

    public struct Arms
    {
        public Arm Right;
        public Arm Left;
    }

    [SerializeField] private Body body;
    [SerializeField] private Weapons weapons;

    private Arms arms;

    private void Awake()
    {
        SetArms();
    }

    protected override void SetColor(Color32 color)
    {
        body.Head.color = color;
        body.RightHand.color = color;
        body.Chest.color = color;
        body.RightLeg.color = color;
        body.Underware.color = color;
        body.LeftLeg.color = color;
        body.LeftHand.color = color;
    }

    private void FixedUpdate()
    {
        FaceTarget();
    }

    public override void Equip(Weapon weapon, Weapon.Hand hand)
    {
        if (hand == Weapon.Hand.Left)
        {
            weapons.Left.sprite = weapon.hands.Contains(Weapon.Hand.Both) ? null : weapon.sprite;
        }
        else
        {
            weapons.Right.sprite = weapon.sprite;
        }
    }

    public override void Unequip(Weapon.Hand hand)
    {
        if (hand == Weapon.Hand.Left)
        {
            weapons.Left.sprite = null;
        }
        else
        {
            weapons.Right.sprite = null;
        }
    }

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
            Wield();
        }
        else
        {
            EnterBattle(target);
        }
    }

    public override void EnterBattle(Character target)
    {
        base.EnterBattle(target);
        Wield();
    }

    public override void ExitBattle()
    {
        base.ExitBattle();
        Unwield();
        ResetArms();
    }

    public override void Flip(bool flip)
    {
        base.Flip(flip);
        SwapHands(flip);
        FaceTarget();
    }

    public override void Aim(Character target)
    {
        if (character.Equipment.Weapons.Right != null)
        {
            character.Equipment.Weapons.Right.Aim(arms, target);
        }

        if (character.Equipment.Weapons.Left != null)
        {
            character.Equipment.Weapons.Left.Aim(arms, target);
        }
    }

    protected override void SetSprites()
    {
        body.RightHand.sprite = character.Data.Graphics.Hand;
        body.LeftHand.sprite = character.Data.Graphics.Hand;
        body.Head.sprite = character.Data.Graphics.Head;
        body.Chest.sprite = character.Data.Graphics.Chest;
        body.Underware.sprite = character.Data.Graphics.Underware;
        body.RightLeg.sprite = character.Data.Graphics.Leg;
        body.LeftLeg.sprite = character.Data.Graphics.Leg;
    }

    private void Wield()
    {
        weapons.Left.enabled = true;
        weapons.Right.enabled = true;
    }

    private void Unwield()
    {
        weapons.Left.enabled = false;
        weapons.Right.enabled = false;
    }

    private void ResetArms()
    {
        arms.Right.Transform.localRotation = Quaternion.identity;
        arms.Right.Hand.sortingOrder = arms.Right.OriginalSortingOrder;
        arms.Right.Transform.gameObject.SetActive(true);

        arms.Left.Transform.localRotation = Quaternion.identity;
        arms.Left.Hand.sortingOrder = arms.Left.OriginalSortingOrder;
        arms.Left.Transform.gameObject.SetActive(true);
    }

    private void FaceTarget()
    {
        if (character.Target == null)
        {
            ResetHead();
            return;
        }

        Vector3 direction = (character.Target.transform.position - transform.position).normalized;

        bool isHeadFlipped = body.Head.transform.localRotation.y != 0;
        bool isFacingRight = (IsFlipped && isHeadFlipped) || (!IsFlipped && !isHeadFlipped);
        bool shouldFaceRight = direction.x > 0;
        bool shouldFlipHead = isFacingRight != shouldFaceRight;

        if (shouldFlipHead)
        {
            body.Head.transform.Rotate(0, isHeadFlipped ? -FLIP_ANGLE : FLIP_ANGLE, 0);
        }
    }

    private void ResetHead()
    {
        if (body.Head.transform.localRotation.y != 0)
        {
            body.Head.transform.Rotate(0, -FLIP_ANGLE, 0);
        }
    }

    private void SwapHands(bool flip)
    {
        Vector3 newRotation = new(0, flip ? FLIP_ANGLE : -FLIP_ANGLE, 0);

        arms.Right.Transform.transform.Rotate(newRotation);
        arms.Left.Transform.transform.Rotate(newRotation);

        if (!character.IsInBattle)
        {
            body.LeftHand.transform.Rotate(newRotation);
            body.RightHand.transform.Rotate(newRotation);
        }

        (weapons.Right.sortingOrder, weapons.Left.sortingOrder) = (weapons.Left.sortingOrder, weapons.Right.sortingOrder);
        (body.LeftHand.sortingOrder, body.RightHand.sortingOrder) = (body.RightHand.sortingOrder, body.LeftHand.sortingOrder);
    }

    private void SetArms()
    {
        arms.Right.Transform = body.RightHand.transform.parent;
        arms.Right.Hand = body.RightHand;
        arms.Right.Weapon = weapons.Right;
        arms.Right.OriginalSortingOrder = weapons.Right.sortingOrder;

        arms.Left.Transform = body.LeftHand.transform.parent;
        arms.Left.Hand = body.LeftHand;
        arms.Left.Weapon = weapons.Left;
        arms.Left.OriginalSortingOrder = weapons.Left.sortingOrder;
    }
}