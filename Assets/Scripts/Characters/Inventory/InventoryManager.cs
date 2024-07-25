using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Bag[] bags;
    private RuntimeBag[] equipedBags;

    private void Awake()
    {
        equipedBags = new RuntimeBag[bags.Length];
        
        EquipDefaultBags();
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
        equipedBags[index] = new RuntimeBag(bag);
    }

    public void RemoveBag(int index)
    {
        bags[index] = null;
        equipedBags[index] = null;
    }

    public void ReplaceBag(int index, Bag newBag)
    {
        RuntimeBag oldInventoryBag = equipedBags[index];
        RuntimeBag newInventoryBag = new(newBag);

        oldInventoryBag?.Move(newInventoryBag);

        bags[index] = newBag;
        equipedBags[index] = newInventoryBag;
    }

    public void SwapBags(int index1, int index2)
    {
        (bags[index2], bags[index1]) = (bags[index1], bags[index2]);
        (equipedBags[index2], equipedBags[index1]) = (equipedBags[index1], equipedBags[index2]);
    }

    public int AddItem(Item item, int amount)
    {
        int remaining = amount;

        foreach (RuntimeBag inventoryBag in equipedBags)
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
        return equipedBags[bag].Add(item, amount, slot);
    }

    public int RemoveItem(Item item, int amount)
    {
        int remaining = amount;

        for (int i = equipedBags.Length - 1; i >= 0; i--)
        {
            if (equipedBags[i] != null)
            {
                remaining = equipedBags[i].Remove(item, remaining);

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
        return equipedBags[bag].Remove(slot, amount);
    }

    public void MoveItem(int bag, int destinationBag, int slot, int destiantionSlot)
    {
        RuntimeBag inventoryBag = equipedBags[bag];
        RuntimeBag destinationInventoryBag = equipedBags[destinationBag];
        RuntimeItem inventoryItem = inventoryBag.GetItemByIndex(slot);

        inventoryBag.Remove(slot, inventoryItem.Amount);
        destinationInventoryBag.Add(inventoryItem.Item, inventoryItem.Amount, destiantionSlot);
    }

    private void EquipDefaultBags()
    {
        for (int i = 0; i < bags.Length; i++)
        {
            if (bags[i] != null)
            {
                equipedBags[i] = new RuntimeBag(bags[i]);
            }

        }
    }

    private bool GetEmptyBagSlot(out int index)
    {
        for (int i = 0; i < equipedBags.Length; i++)
        {
            if (equipedBags[i] == null)
            {
                index = i;
                return true;
            }
        }

        index = -1;
        return false;
    }
}
