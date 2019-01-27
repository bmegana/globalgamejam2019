using UnityEngine;
using UnityEngine.UI;

public class DummyLootBehavior : MonoBehaviour
{
    public GameObject item;

    private Image itemImageComponent;

    public void UpdateDecoration()
    {
        ItemManager.instance.UpdateDecoration(item);
    }

    private void Awake()
    {
        itemImageComponent =
            gameObject.transform.GetChild(0).GetComponent<Image>();
        if (item == null)
        {
            itemImageComponent.color = Color.clear;
        }
        else
        {
            itemImageComponent.color = Color.white;
            itemImageComponent.sprite =
                item.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
