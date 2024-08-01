using UnityEngine;

[System.Serializable]
public struct HumanoidHand
{
    public Transform Group;
    public SpriteRenderer Skin;
    public SpriteRenderer Equipment;
    public SpriteRenderer Weapon;

    public readonly void Flip(bool flip)
    {
        Group.transform.Rotate(Group.transform.rotation.x, flip ? -180 : 180, Group.transform.rotation.z);
    }
}
