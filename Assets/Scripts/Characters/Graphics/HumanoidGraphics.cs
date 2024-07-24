using System;
using System.Collections;
using UnityEngine;

public class HumanoidGraphics : CharacterBodyGraphics
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

    public BodyRenderers Body;
    public WeaponRenderers Weapons;

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

    public override void SetInBattle(bool inBattle)
    {
        base.SetInBattle(inBattle);

        Weapons.right.gameObject.SetActive(inBattle);
        Weapons.left.gameObject.SetActive(inBattle);

        Body.LeftHand.gameObject.SetActive(!inBattle);
        Body.RightHand.gameObject.SetActive(!inBattle);
    }
}