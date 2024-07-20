using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryBag
{
    private readonly Bag bag;
    private readonly InventoryItem[] slots;
    private readonly Dictionary<Item, int[]> items = new();

    private bool IsFull => items.Count == bag.size;

    public InventoryBag(Bag bag)
    {
        this.bag = bag;
        slots = new InventoryItem[bag.size];
    }

    public void LogBag()
    {
        string empty = "-";
        string result = "";

        Debug.Log($"+---------- {bag.name} ---------+");

        for (int i = 0; i < slots.Length; i++)
        {
            result += "[ " + (slots[i] != null ? slots[i].Amount : empty) + " ]";

            if ((i + 1) % 4 == 0)
            {
                Debug.Log(result);
                result = "";
            }
        }

        if (result != "")
        {
            Debug.Log(result);
        }

        foreach (KeyValuePair<Item, int[]> item in items)
        {
            Debug.Log("[ITEM] " + item.Key.name + " --> slots [" + string.Join(", ", item.Value) + " ]");
        }

        Debug.Log("+--------------------------------+");
    }

    public InventoryItem GetInventoryItem(int slot)
    {
        return slots[slot];
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
            if (slots[i] != null && slots[i].Item == item)
            {
                remaining = Remove(i, remaining);

                if (remaining == 0)
                {
                    break;
                }
            }
        }

        return remaining;
    }

    public void CopyItems(InventoryBag bag)
    {
        for (int i = 0; i < bag.slots.Length; i++)
        {
            if (bag.slots[i] != null)
            {
                Add(bag.slots[i].Item, bag.slots[i].Amount, i);
            }
        }
    }

    public void MoveItems(InventoryBag bag)
    {
        CopyItems(bag);
        bag.Clear();
    }

    public void MoveItem(int slot, InventoryBag destinationBag)
    {
        Add(slots[slot].Item, slots[slot].Amount);
        destinationBag.Remove(slot, slots[slot].Amount);
    }

    public void Clear()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = null;
        }

        items.Clear();
    }

    public void Swap(int slot1, int slot2)
    {
        Item item1 = slots[slot1].Item;
        Item item2 = slots[slot2].Item;
        InventoryItem bagSlot1 = slots[slot1];
        InventoryItem bagSlot2 = slots[slot2];

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
            InventoryItem bagSlot = slots[index];

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

        slots[index] = new InventoryItem(item, amount);

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
}