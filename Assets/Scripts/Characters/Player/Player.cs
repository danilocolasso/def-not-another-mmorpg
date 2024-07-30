using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(InputHandler))]
public class Player : Character
{
    private InputHandler inputHandler;
    private InventoryManager inventoryManager;

    protected override void Awake()
    {
        base.Awake();

        inventoryManager = GetComponent<InventoryManager>();
        inputHandler = GetComponent<InputHandler>();

        movementManager.SetMovement(new InputMovement(inputHandler));
    }

    public bool AddBag(Bag bag)
    {
        return inventoryManager.AddBag(bag);
    }

    public void AddBag(Bag bag, int index)
    {
        inventoryManager.AddBag(bag, index);
    }

    public void RemoveBag(int index)
    {
        inventoryManager.RemoveBag(index);
    }

    public int AddItem(Item item, int amount)
    {
        return inventoryManager.AddItem(item, amount);
    }

    public int AddItem(Item item, int amount, int bag, int slot)
    {
        return inventoryManager.AddItem(item, amount, bag, slot);
    }

    public int RemoveItem(Item item, int amount)
    {
        return inventoryManager.RemoveItem(item, amount);
    }

    public void MoveItem(int bagFrom, int bagTo, int slotFrom, int slotTo)
    {
        inventoryManager.MoveItem(bagFrom, bagTo, slotFrom, slotTo);
    }
}
