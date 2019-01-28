using UnityEngine;
using UnityEngine.UI;

public class LootSlotBehavior : MonoBehaviour
{
    public static LootSlotBehavior instance;

    public GameObject loot;

    private Image itemImageComponent;

    public void AddLoot(GameObject lootToAdd)
    {
        loot = lootToAdd;
        if (itemImageComponent != null)
        {
            itemImageComponent.color = Color.white;
            itemImageComponent.sprite =
                loot.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void UpdateDecoration()
    {
        DecorationManager.instance.UpdateDecoration(loot);
        itemImageComponent.color = Color.clear;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        itemImageComponent =
            gameObject.transform.GetChild(0).GetComponent<Image>();
        if (loot == null)
        {
            itemImageComponent.color = Color.clear;
        }
        else
        {
            itemImageComponent.color = Color.white;
            itemImageComponent.sprite =
                loot.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
