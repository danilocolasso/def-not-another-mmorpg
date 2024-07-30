using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public struct HumanoidArm
{
    public SortingGroup Group;
    public HumanoidHand Hand;
    [HideInInspector] public int DefaultSorting;

    public readonly void Show()
    {
        Group.gameObject.SetActive(true);
    }

    public readonly void Hide()
    {
        Group.gameObject.SetActive(false);
    }

    public readonly void Reset()
    {
        Group.transform.localRotation = Quaternion.identity;
        Group.sortingOrder = DefaultSorting;
        Show();
    }

    public readonly void Flip(bool flip)
    {
        Group.transform.Rotate(0, flip ? -180 : 180, 0);
    }
}