using UnityEngine;
using System.Collections.Generic;

public class InventoryBag
{
    private readonly Bag bag;
    private readonly BagSlot[] slots;
    private readonly Dictionary<Item, int[]> indexes = new();

    private bool IsFull => indexes.Count == bag.size;

    public InventoryBag(Bag bag)
    {
        this.bag = bag;
        slots = new BagSlot[bag.size];
    }

    public int Add(Item item, int amount, int slot)
    {
        int remaining = amount;

        if (slots[slot] == null)
        {
            remaining = CreateSlot(item, amount, slot);
        }

        if (remaining > 0)
        {
            return Add(item, remaining);
        }

        return remaining;
    }

    public int Add(Item item, int amount)
    {
        int remaining = Stack(item, amount);

        while (remaining > 0 && !IsFull)
        {
            if (!GetEmptySlotIndex(out int index))
            {
                break;
            }

            remaining = CreateSlot(item, amount, index);
        }

        return remaining;
    }

    public int Remove(Item item, int amount, int slot)
    {
        BagSlot bagSlot = slots[slot];
        int remaining = amount;

        if (bagSlot.Amount > amount)
        {
            bagSlot.Unstack(amount);
            return 0;
        }

        indexes.Remove(item);
        slots[slot] = null;
        remaining -= bagSlot.Amount;

        return remaining;
    }

    public int Remove(Item item, int amount = 1)
    {
        int remaining = amount;

        foreach (int index in indexes[item])
        {
            remaining = Remove(item, remaining, index);

            if (remaining == 0)
            {
                break;
            }
        }

        return remaining;
    }

    public void Swap(int slot1, int slot2)
    {
        Item item1 = slots[slot1].Item;
        Item item2 = slots[slot2].Item;
        BagSlot bagSlot1 = slots[slot1];
        BagSlot bagSlot2 = slots[slot2];

        Remove(item1, bagSlot1.Amount, slot1);
        Remove(item2, bagSlot2.Amount, slot2);
        Add(item1, bagSlot1.Amount, slot2);
        Add(item2, bagSlot2.Amount, slot1);
    }

    private int Stack(Item item, int amount)
    {
        if (!indexes.ContainsKey(item))
        {
            return amount;
        }

        foreach (int index in indexes[item])
        {
            BagSlot bagSlot = slots[index];

            if (!bagSlot.CanStack())
            {
                continue;
            }

            amount = bagSlot.Stack(amount);
        }

        return amount;
    }

    private int CreateSlot(Item item, int amount, int index)
    {
        int remaining = Mathf.Max(0, amount - item.maxStack);
        amount = Mathf.Min(item.maxStack, amount);

        slots[index] = new BagSlot(item, amount);
        indexes.Add(item, new int[] { index });

        return remaining;
    }

    private bool GetEmptySlotIndex(out int index)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                index = i;
                return true;
            }
        }

        index = -1;
        return false;
    }
}