using System;
using UnityEngine;

public class HumanoidGraphics : AbstractCharacterGraphics
{
    [Serializable]
    public struct BodyRenderers
    {
        public SpriteRenderer RightHand;
        public SpriteRenderer Head;
        public SpriteRenderer Chest;
        public SpriteRenderer RightLeg;
        public SpriteRenderer Under;
        public SpriteRenderer LeftLeg;
        public SpriteRenderer LeftHand;
    }

    [Serializable]
    public struct WeaponRenderers
    {
        public SpriteRenderer right;
        public SpriteRenderer left;
    }

    private struct HandsOriginalPosition
    {
        public Vector3 right;
        public Vector3 left;
    }

    private GameObject rightArm;
    private GameObject leftArm;
    private HandsOriginalPosition handsOriginalPosition;

    public BodyRenderers Body;
    public WeaponRenderers Weapons;

    public override void Initialize(Character character)
    {
        base.Initialize(character);
        SetArms();
    }

    public override void SetColor(Color32 color)
    {
        Body.Head.color = color;
        Body.RightHand.color = color;
        Body.Chest.color = color;
        Body.RightLeg.color = color;
        Body.Under.color = color;
        Body.LeftLeg.color = color;
        Body.LeftHand.color = color;
    }

    public override void ExitBattle()
    {
        base.ExitBattle();
        ResetRightArm();
    }

    private void FixedUpdate()
    {
        if (character.IsInBattle)
        {
            UpdateRightArm();
            UpdateLeftArm();
        }

        FaceTarget();
    }

    private void SetArms()
    {
        rightArm = Body.RightHand.transform.parent.gameObject;
        leftArm = Body.LeftHand.transform.parent.gameObject;

        handsOriginalPosition.right = Body.RightHand.transform.localPosition;
        handsOriginalPosition.left = Body.LeftHand.transform.localPosition;
    }

    private void UpdateRightArm()
    {
        if (character.Target != null)
        {
            Vector3 direction = character.Target.transform.position - transform.position;
            rightArm.transform.right = -direction;
            return;
        }

        if (rightArm.transform.right != Vector3.right)
        {
            ResetRightArm();
        }
    }

    private void UpdateLeftArm()
    {
        // TODO implement
    }

    private void ResetRightArm()
    {
        rightArm.transform.right = Vector3.right;
    }

    public override void Flip(bool flip)
    {
        base.Flip(flip);
        FaceTarget();
        SwapHands();
    }

    private void FaceTarget()
    {
        if (character.Target == null)
        {
            ResetHead();
            return;
        }

        Vector3 direction = (character.Target.transform.position - transform.position).normalized;

        bool headIsFlipped = Body.Head.transform.localRotation.y != 0;
        bool isFacingRight = (IsFlipped && headIsFlipped) || (!IsFlipped && !headIsFlipped);
        bool shouldFaceRight = direction.x > 0;
        bool shouldFlipHead = isFacingRight != shouldFaceRight;

        if (shouldFlipHead)
        {
            Body.Head.transform.Rotate(0, headIsFlipped ? -FLIP_ANGLE : FLIP_ANGLE, 0);
        }
    }

    private void ResetHead()
    {
        if (Body.Head.transform.localRotation.y != 0)
        {
            Body.Head.transform.Rotate(Vector3.zero);
        }
    }

    private void SwapHands()
    {
        // TODO: swap in the animator, because the animation is overwriting the position
        // (Body.RightHand.transform.position, Body.LeftHand.transform.position) = (Body.LeftHand.transform.position, Body.RightHand.transform.position);
        (Body.LeftHand.sortingOrder, Body.RightHand.sortingOrder) = (Body.RightHand.sortingOrder, Body.LeftHand.sortingOrder);
    }
}