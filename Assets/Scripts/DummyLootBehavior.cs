using UnityEngine;
using UnityEngine.UI;

public class DummyLootBehavior : MonoBehaviour
{
    public GameObject itemPrefab;
    private GameObject itemInstance;

    private Image itemImageComponent;

    public void UpdateDecoration()
    {
        DecorationManager.instance.UpdateDecoration(itemInstance);
        itemImageComponent.color = Color.clear;
        itemInstance = null;
    }

    private void Awake()
    {
        if (itemPrefab != null)
        {
            itemInstance = Instantiate(itemPrefab);
            itemInstance.SetActive(false);
        }
        itemImageComponent =
            gameObject.transform.GetChild(0).GetComponent<Image>();
        if (itemInstance == null)
        {
            itemImageComponent.color = Color.clear;
        }
        else
        {
            itemImageComponent.color = Color.white;
            itemImageComponent.sprite =
                itemInstance.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
