using UnityEngine;
using UnityEngine.UI;

public class InGameDecorDisplay : MonoBehaviour
{
    public int uniqueDecorListIndex;
    private Image imageComponent;

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
    }

    private void Start()
    {
        GameObject decor =
            DecorationManager.instance.decorationsList[uniqueDecorListIndex];
        if (decor != null)
        {
            imageComponent.sprite =
                decor.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void UpdateDisplay(Sprite sprite)
    {
        imageComponent.sprite = sprite;
    }
}
