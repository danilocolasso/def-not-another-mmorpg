using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(ExperienceManager))]
[RequireComponent(typeof(InputHandler))]
public class Player : Character
{
    private InputHandler inputHandler;
    private InventoryManager inventoryManager;
    private ExperienceManager experienceManager;

    protected override void Awake()
    {
        base.Awake();

        inventoryManager = GetComponent<InventoryManager>();
        experienceManager = GetComponent<ExperienceManager>();
        inputHandler = GetComponent<InputHandler>();

        SetMovement(new InputMovement(inputHandler));
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

    public void GainExperience(int amount)
    {
        experienceManager.GainExperience(amount);
    }

    protected void Update()
    {
        Testing();
    }

    public Item testItem;
    private void Testing()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            testItem.Use(this);
            Debug.Log("Used " + testItem.name);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            AddItem(testItem, 3);
            Debug.Log("Added 3 " + testItem.name + " to the first available slot");
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            int amount = Random.Range(1, 5);
            int bag = Random.Range(0, 3);
            int slot = Random.Range(0, 5);

            AddItem(testItem, amount, bag, slot);
            Debug.Log("Added " + amount + " " + testItem.name + " to bag " + bag + " slot " + slot);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            RemoveItem(testItem, 2);
            Debug.Log("Removed 2 " + testItem.name);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            MoveItem(0, 1, 0, 4);
            Debug.Log("Moved item from bag 0 slot 0 to bag 1 slot 4");
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            GainExperience(12);
            Debug.Log("Gained 12 experience");
        }

        if (Target)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack(Target);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                UseAbility("Arrow Shot", Target);
            }
        }
    }
}
