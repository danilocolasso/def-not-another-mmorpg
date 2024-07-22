using UnityEngine;

public class PlayerDebbuger : MonoBehaviour
{
    public Player player;
    public Item testItem;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            testItem.Use(player);
            Debug.Log("Used " + testItem.name);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            player.AddItem(testItem, 3);
            Debug.Log("Added 3 " + testItem.name + " to the first available slot");
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            int amount = Random.Range(1, 5);
            int bag = Random.Range(0, 3);
            int slot = Random.Range(0, 5);

            player.AddItem(testItem, amount, bag, slot);
            Debug.Log("Added " + amount + " " + testItem.name + " to bag " + bag + " slot " + slot);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            player.RemoveItem(testItem, 2);
            Debug.Log("Removed 2 " + testItem.name);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            player.MoveItem(0, 1, 0, 4);
            Debug.Log("Moved item from bag 0 slot 0 to bag 1 slot 4");
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            player.GainExperience(12);
            Debug.Log("Gained 12 experience");
        }

        if (player.Target)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.Attack(player.Target);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                player.UseAbility("Arrow Shot", player.Target);
            }
        }
    }
}
