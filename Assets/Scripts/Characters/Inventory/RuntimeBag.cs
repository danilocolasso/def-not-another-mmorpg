using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RuntimeBag
{
    private readonly Bag bag;
    private readonly RuntimeItem[] slots;
    private readonly Dictionary<Item, int[]> items = new();

    private bool IsFull => items.Count == bag.size;

    public RuntimeBag(Bag bag)
    {
        this.bag = bag;
        slots = new RuntimeItem[bag.size];
    }

    public RuntimeItem GetItemByIndex(int index)
    {
        return slots[index];
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

            remaining = CreateSlot(item, remaining, index);
        }

        return remaining;
    }

    public int Remove(int slot, int amount)
    {
        Item item = slots[slot].Item;

        if (slots[slot].Unstack(amount, out int remaining))
        {
            RemoveSlot(slot);
        }

        if (remaining > 0)
        {
            return Remove(item, remaining);
        }

        return remaining;
    }

    public int Remove(Item item, int amount)
    {
        int remaining = amount;

        for (int i = slots.Length - 1; i >= 0; i--)
        {
            if (slots[i]?.Item != item)
            {
                continue;
            }

            remaining = Remove(i, remaining);

            if (remaining == 0)
            {
                break;
            }
        }

        return remaining;
    }

    public void Copy(RuntimeBag bag)
    {
        for (int i = 0; i < bag.slots.Length; i++)
        {
            if (bag.slots[i] != null)
            {
                Add(bag.slots[i].Item, bag.slots[i].Amount, i);
            }
        }
    }

    public void Move(RuntimeBag bag)
    {
        Copy(bag);
        bag.Clear();
    }

    public void MoveItem(int slot, RuntimeBag destinationBag)
    {
        Add(slots[slot].Item, slots[slot].Amount);
        destinationBag.Remove(slot, slots[slot].Amount);
    }

    public void Swap(int slot1, int slot2)
    {
        Item item1 = slots[slot1].Item;
        Item item2 = slots[slot2].Item;
        RuntimeItem bagSlot1 = slots[slot1];
        RuntimeItem bagSlot2 = slots[slot2];

        Remove(slot1, bagSlot1.Amount);
        Remove(slot2, bagSlot2.Amount);
        Add(item1, bagSlot1.Amount, slot2);
        Add(item2, bagSlot2.Amount, slot1);
    }

    private int Stack(Item item, int amount)
    {
        if (!items.ContainsKey(item))
        {
            return amount;
        }

        foreach (int index in items[item])
        {
            RuntimeItem bagSlot = slots[index];

            if (bagSlot.CanStack())
            {
                amount = bagSlot.Stack(amount);
            }
        }

        return amount;
    }

    private int CreateSlot(Item item, int amount, int index)
    {
        int remaining = Mathf.Max(0, amount - item.maxStack);
        amount = Mathf.Min(item.maxStack, amount);

        slots[index] = new RuntimeItem(item, amount);

        if (items.ContainsKey(item))
        {
            items[item] = items[item].Append(index).ToArray();
        }
        else
        {
            items.Add(item, new int[] { index });
        }

        return remaining;
    }

    private void RemoveSlot(int slot)
    {
        items[slots[slot].Item] = items[slots[slot].Item].Where(i => i != slot).ToArray();

        if (items[slots[slot].Item].Length == 0)
        {
            items.Remove(slots[slot].Item);
        }

        slots[slot] = null;
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

    private void Clear()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = null;
        }

        items.Clear();
    }
}