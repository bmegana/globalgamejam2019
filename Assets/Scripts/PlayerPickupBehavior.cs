using UnityEngine;

public class PlayerPickupBehavior : MonoBehaviour
{
    public GameObject item;
    public LootSlotBehavior lootSlot;

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Loot":
                lootSlot.AddLoot(col.gameObject);
                col.gameObject.SetActive(false);
                break;
        }
    }
}
