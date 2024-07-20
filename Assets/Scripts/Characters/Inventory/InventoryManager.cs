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
    }

    public void RemoveBag(int index)
    {
        bags[index] = null;
        inventoryBags[index] = null;
    }

    public void ReplaceBag(int index, Bag newBag)
    {
        InventoryBag oldInventoryBag = inventoryBags[index];
        InventoryBag newInventoryBag = new(newBag);

        oldInventoryBag?.MoveItems(newInventoryBag);

        bags[index] = newBag;
        inventoryBags[index] = newInventoryBag;
    }

    public void SwapBags(int index1, int index2)
    {
        (bags[index2], bags[index1]) = (bags[index1], bags[index2]);
        (inventoryBags[index2], inventoryBags[index1]) = (inventoryBags[index1], inventoryBags[index2]);
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

        return remaining;
    }

    public int AddItem(Item item, int amount, int bag, int slot)
    {
        return inventoryBags[bag].Add(item, amount, slot);
    }

    public int RemoveItem(Item item, int amount)
    {
        int remaining = amount;

        for (int i = inventoryBags.Length - 1; i >= 0; i--)
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

        return remaining;
    }

    public int RemoveItem(int bag, int slot, int amount)
    {
        return inventoryBags[bag].Remove(slot, amount);
    }

    public void MoveItem(int bag, int destinationBag, int slot, int destiantionSlot)
    {
        InventoryBag inventoryBag = inventoryBags[bag];
        InventoryBag destinationInventoryBag = inventoryBags[destinationBag];
        InventoryItem inventoryItem = inventoryBag.GetInventoryItem(slot);

        inventoryBag.Remove(slot, inventoryItem.Amount);
        destinationInventoryBag.Add(inventoryItem.Item, inventoryItem.Amount, destiantionSlot);
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
