
using UnityEngine;

[CreateAssetMenu(fileName = "New Bag", menuName = "Scriptable Objects/Items/Container/Bag")]
public class Bag : Item
{
    [Range(1, 32)] public int slots;

    public override void Use(Character character)
    {
        InventoryManager inventory = character.GetComponent<InventoryManager>();
        inventory.AddBag(this);
    }
}
