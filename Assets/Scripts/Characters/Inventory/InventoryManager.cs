using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private const int MAX_BAGS = 4;
    [SerializeField] private Dictionary<Bag, List<Item>> bags = new(MAX_BAGS);

    public bool AddBag(Bag bag)
    {
        if (bags.Count >= MAX_BAGS)
        {
            Debug.Log("Cannot add more bags");
            return false;
        }

        bags.Add(bag, new List<Item>(bag.size));
        Debug.Log($"Added {bag.name} to bags");

        return true;
    }

    public bool RemoveBag(Bag bag)
    {
        return bags.Remove(bag);
    }

    public void ReplaceBag(Bag oldBag, Bag newBag)
    {
        if (bags.Remove(oldBag))
        {
            bags.Add(newBag, new List<Item>(newBag.size));
        }
    }

    public bool AddItem(Item item)
    {
        foreach (var bag in bags)
        {
            if (AddItem(item, bag.Key))
            {
                return true;
            }
        }

        Debug.Log("No space in bags");
        return false;
    }

    public bool AddItem(Item item, Bag bag)
    {
        if (HasSpaceInBag(bag))
        {
            bags[bag].Add(item);
            Debug.Log($"Added {item.name} to {bag.name}");
            return true;
        }

        return false;
    }

    public bool RemoveItem(Item item)
    {
        foreach (var bag in bags)
        {
            if (RemoveItem(item, bag.Key))
            {
                return true;
            }
        }

        return false;
    }

    public bool RemoveItem(Item item, Bag bag)
    {
        Debug.Log($"Removed {item.name} from {bag.name}");
        return bags[bag].Remove(item);
    }

    public bool MoveItem(Item item, Bag from, Bag to)
    {
        if (!RemoveItem(item, from))
        {
            return false;
        }

        if (!AddItem(item, to))
        {
            AddItem(item, from);
            return false;
        }

        return true;
    }

    private bool HasSpaceInBag(Bag bag)
    {
        return bags[bag].Count < bag.size;
    }
}
