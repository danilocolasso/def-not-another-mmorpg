using System;
using System.Collections;
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

    public override void EnterBattle(Character target)
    {
        base.EnterBattle(target);

        Weapons.right.gameObject.SetActive(false);
        Weapons.left.gameObject.SetActive(false);
    }

    public override void ExitBattle()
    {
        base.ExitBattle();

        Weapons.right.gameObject.SetActive(true);
        Weapons.left.gameObject.SetActive(true);
    }

    private void SetArms()
    {
        rightArm = Body.RightHand.transform.parent.gameObject;
        leftArm = Body.LeftHand.transform.parent.gameObject;
    }
}