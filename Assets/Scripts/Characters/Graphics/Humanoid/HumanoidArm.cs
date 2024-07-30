using UnityEngine;

[System.Serializable]
public struct HumanoidArm
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