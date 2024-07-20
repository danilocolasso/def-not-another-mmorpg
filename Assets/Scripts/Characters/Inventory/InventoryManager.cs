using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private const int MAX_BAGS = 4;
    [SerializeField] private Bag[] bags = new Bag[MAX_BAGS];
    private readonly InventoryBag[] inventoryBags = new InventoryBag[MAX_BAGS];

    private void Awake()
    {
        AddDefaultBags();
    }

    public void LogBags()
    {
        for (int i = 0; i < inventoryBags.Length; i++)
        {
            inventoryBags[i]?.LogBag();
        }
    }

    public bool AddBag(Bag bag)
    {
        if (!GetEmptyBagSlot(out int index))
        {
            Debug.Log("Cannot add more bags");
            return false;
        }

        AddBag(bag, index);

        return true;
    }

    public void AddBag(Bag bag, int index)
    {
        bags[index] = bag;
        inventoryBags[index] = new InventoryBag(bag);

        Debug.Log($"Added {bag.name} to bags");
        LogBags();
    }

    public void RemoveBag(int index)
    {
        bags[index] = null;
        inventoryBags[index] = null;

        Debug.Log($"Removed bag at index {index}");
        LogBags();
    }

    public void ReplaceBag(int index, Bag newBag)
    {
        InventoryBag oldInventoryBag = inventoryBags[index];
        InventoryBag newInventoryBag = new(newBag);

        oldInventoryBag?.MoveItems(newInventoryBag);

        bags[index] = newBag;
        inventoryBags[index] = newInventoryBag;

        Debug.Log($"Replaced bag at index {index}");
        LogBags();
    }

    public void SwapBags(int index1, int index2)
    {
        (bags[index2], bags[index1]) = (bags[index1], bags[index2]);
        (inventoryBags[index2], inventoryBags[index1]) = (inventoryBags[index1], inventoryBags[index2]);

        Debug.Log($"Swapped bags at indexes {index1} and {index2}");
        LogBags();
    }

    public int AddItem(Item item, int amount)
    {
        int remaining = amount;

        foreach (InventoryBag inventoryBag in inventoryBags)
        {
            if (inventoryBag != null)
            {
                remaining = inventoryBag.Add(item, remaining);

                if (remaining == 0)
                {
                    break;
                }
            }
        }

        Debug.Log($"Added {item.name} x{amount - remaining}");
        LogBags();

        return remaining;
    }

    public int AddItem(Item item, int amount, int bag, int slot)
    {
        // return inventoryBags[bag].Add(item, amount, slot); // TODO: Uncomment and logs below

        var foo = inventoryBags[bag].Add(item, amount, slot);
        LogBags();
        return foo;
    }

    public int RemoveItem(Item item, int amount)
    {
        int remaining = amount;

        for (int i = 0; i < inventoryBags.Length; i++)
        {
            if (inventoryBags[i] != null)
            {
                remaining = inventoryBags[i].Remove(item, remaining);

                if (remaining == 0)
                {
                    break;
                }
            }
        }

        Debug.Log($"Removed {item.name} x{amount - remaining}");
        LogBags();

        return remaining;
    }

    public int RemoveItem(int bag, int slot, int amount)
    {
        // return inventoryBags[bag].Remove(slot, amount); // TODO: Uncomment and logs below

        var foo = inventoryBags[bag].Remove(slot, amount);
        Debug.Log($"Removed {amount - foo} from bag {bag} slot {slot}");
        LogBags();
        return foo;
    }

    public void MoveItem(int bag, int destinationBag, int slot, int destiantionSlot)
    {
        InventoryBag inventoryBag = inventoryBags[bag];
        InventoryBag destinationInventoryBag = inventoryBags[destinationBag];
        InventoryItem inventoryItem = inventoryBag.GetInventoryItem(slot);

        inventoryBag.Remove(slot, inventoryItem.Amount);
        destinationInventoryBag.Add(inventoryItem.Item, inventoryItem.Amount, destiantionSlot);

        Debug.Log($"Moved {inventoryItem.Item.name} x{inventoryItem.Amount} from bag {bag} slot {slot} to bag {destinationBag} slot {destiantionSlot}");
        LogBags();
    }

    private void AddDefaultBags()
    {
        for (int i = 0; i < bags.Length; i++)
        {
            if (bags[i] != null)
            {
                inventoryBags[i] = new InventoryBag(bags[i]);
            }

        }

        LogBags();
    }

    private bool GetEmptyBagSlot(out int index)
    {
        for (int i = 0; i < inventoryBags.Length; i++)
        {
            if (inventoryBags[i] == null)
            {
                index = i;
                return true;
            }
        }

        index = -1;
        return false;
    }
}
