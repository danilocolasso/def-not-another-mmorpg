using UnityEngine;

public class RuntimeItem
{
    public readonly Item Item;
    public int Amount { get; private set; }

    public RuntimeItem(Item item, int amount)
    {
        Item = item;
        Amount = amount;
    }

    public void Use(Character character)
    {
        Item.Use(character);
    }

    public bool CanStack()
    {
        return Item.CanStack && Amount < Item.maxStack;
    }

    public int Stack(int amount)
    {
        int remaining = Mathf.Max(0, Amount + amount - Item.maxStack);
        Amount = Mathf.Min(Item.maxStack, Amount + amount);

        return remaining;
    }

    public bool Unstack(int amount, out int remaining)
    {
        remaining = Mathf.Max(0, amount - Amount);
        Amount = Mathf.Max(0, Amount - amount);

        return Amount == 0;
    }
}