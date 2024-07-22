using UnityEngine;

[System.Serializable]
public class HumanoidCharacter
{
    public SpriteRenderer Head;
    public SpriteRenderer RightHand;
    public SpriteRenderer Chest;
    public SpriteRenderer RightLeg;
    public SpriteRenderer Under;
    public SpriteRenderer LeftLeg;
    public SpriteRenderer LeftHand;

    public void SetSkinColor(Color32 color)
    {
        Head.color = color;
        RightHand.color = color;
        Chest.color = color;
        RightLeg.color = color;
        Under.color = color;
        LeftLeg.color = color;
        LeftHand.color = color;
    }
}