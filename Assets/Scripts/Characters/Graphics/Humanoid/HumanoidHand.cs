using UnityEngine;

[System.Serializable]
public struct HumanoidHand
{
    public Transform Group;
    public SpriteRenderer Skin;
    public SpriteRenderer Equipment;
    public SpriteRenderer Weapon;

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
        Group.transform.Rotate(0, flip ? -180 : 180, 0);
    }
}
