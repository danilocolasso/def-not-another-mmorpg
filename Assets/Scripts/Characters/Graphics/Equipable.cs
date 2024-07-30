using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public struct Equipable
{
    public SortingGroup Group;
    public SpriteRenderer Skin;
    public SpriteRenderer Equipment;
    [HideInInspector] public int DefaultSortingOrder;

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
        Group.transform.Rotate(0, flip ? 180 : -180, 0);
    }
}
