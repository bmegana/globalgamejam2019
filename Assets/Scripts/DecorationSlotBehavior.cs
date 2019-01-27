using UnityEngine;
using UnityEngine.UI;

public class DecorationSlotBehavior : MonoBehaviour
{
    public int uniqueDecorationIndex;
    public GameObject item;

    private Image itemImageComponent;

    public void SetDecorationIndexToEdit(int index)
    {
        DecorationManager.instance.SetDecorationIndexToEdit(index);
    }

    public void UpdateSlot(Sprite lootSprite)
    {
        itemImageComponent.color = Color.white;
        itemImageComponent.sprite = lootSprite;
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

    public Sprite getSprite()
    {
        return itemImageComponent.sprite;
    }
}
