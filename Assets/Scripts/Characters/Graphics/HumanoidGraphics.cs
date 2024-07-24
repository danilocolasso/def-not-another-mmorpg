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

    private GameObject rightArm;
    private GameObject leftArm;
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
        }
    }

    private void SetArms()
    {
        rightArm = Body.RightHand.transform.parent.gameObject;
        leftArm = Body.LeftHand.transform.parent.gameObject;
    }

    private void UpdateRightArm()
    {
        rightArm.transform.right = -(character.Target.transform.position - character.transform.position);
    }

    private void ResetRightArm()
    {
        rightArm.transform.right = Vector3.right;
    }
}