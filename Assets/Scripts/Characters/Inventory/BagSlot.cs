
using UnityEngine;

public class BagSlot
{
    public readonly Item Item;
    public int Amount { get; private set; }

    public BagSlot(Item item, int amount = 1)
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

    public int Unstack(int amount)
    {
        int remaining = Mathf.Max(0, amount - Amount);
        Amount = Mathf.Max(0, Amount - amount);

        return remaining;
    }
}