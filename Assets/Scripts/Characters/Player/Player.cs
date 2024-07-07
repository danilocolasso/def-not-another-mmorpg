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

    public bool RemoveBag(Bag bag)
    {
        return inventoryManager.RemoveBag(bag);
    }

    public bool AddItem(Item item)
    {
        return inventoryManager.AddItem(item);
    }

    public bool AddItem(Item item, Bag bag)
    {
        return inventoryManager.AddItem(item, bag);
    }

    public bool RemoveItem(Item item)
    {
        return inventoryManager.RemoveItem(item);
    }

    public bool RemoveItem(Item item, Bag bag)
    {
        return inventoryManager.RemoveItem(item, bag);
    }

    public bool MoveItem(Item item, Bag fromBag, Bag toBag)
    {
        return inventoryManager.MoveItem(item, fromBag, toBag);
    }

    public void GainExperience(int amount)
    {
        experienceManager.GainExperience(amount);
    }

    public Item testItem;
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            testItem.Use(this);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItem(testItem);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            RemoveItem(testItem);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GainExperience(12);
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
