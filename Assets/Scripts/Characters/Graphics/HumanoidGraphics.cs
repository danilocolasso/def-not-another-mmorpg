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

    private struct OriginalPosition
    {
        public Vector3 rightHand;
        public Vector3 leftHand;
    }

    private GameObject rightArm;
    private GameObject leftArm;
    private OriginalPosition hands;

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
    }

    private void SetArms()
    {
        rightArm = Body.RightHand.transform.parent.gameObject;
        leftArm = Body.LeftHand.transform.parent.gameObject;

        hands.rightHand = Body.RightHand.transform.localPosition;
        hands.leftHand = Body.LeftHand.transform.localPosition;
    }

    private void UpdateRightArm()
    {
        Vector2 direction = -(character.Target.transform.position - character.transform.position);
        rightArm.transform.right = direction;
    }

    private void UpdateLeftArm()
    {
        Vector2 direction = -(character.Target.transform.position - character.transform.position);
    }

    private void ResetRightArm()
    {
        rightArm.transform.right = Vector3.right;
    }

    public override void Flip(bool flip)
    {
        base.Flip(flip);
        SwapHands();
    }

    private void SwapHands()
    {
        Body.LeftHand.transform.localPosition = new Vector3(hands.rightHand.x, hands.rightHand.y, hands.leftHand.z);
        Body.RightHand.transform.localPosition = new Vector3(hands.leftHand.x, hands.leftHand.y, hands.rightHand.z);
    
        (Body.LeftHand.sortingOrder, Body.RightHand.sortingOrder) = (Body.RightHand.sortingOrder, Body.LeftHand.sortingOrder);
    }
}