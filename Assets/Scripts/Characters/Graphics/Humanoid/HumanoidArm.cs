using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public struct HumanoidArm
{
    public SortingGroup Group;
    public HumanoidHand Hand;
    [HideInInspector] public int DefaultSorting;

    public readonly void Reset()
    {
        Group.transform.localRotation = Quaternion.identity;
        Group.sortingOrder = DefaultSorting;
        Group.gameObject.SetActive(true);
    }

    public readonly void Flip(bool flip)
    {
        Group.transform.Rotate(Group.transform.rotation.x, flip ? -180 : 180, Group.transform.rotation.z);
        Hand.Flip(flip);
    }
}