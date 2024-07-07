using UnityEngine;

public abstract class Item : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;
    [Range(1, 20)] public int maxStack = 1;

    public abstract void Use(Character character);
}
