using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public ArrayList decorationsList; //TODO: integrate with looting script

    public GameObject decorationsPanel;
    public GameObject decorations;
    public int maxNumDecorations;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            decorationsList = new ArrayList();
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        maxNumDecorations = decorations.transform.childCount;
    }

    public void DisplayDecorateMenu()
    {
        decorationsPanel.SetActive(true);
    }

    public void DropDecoration()
    {
        
    }

    public void UpdateDecorationList(ArrayList chosenLootList)
    {
        foreach (GameObject loot in chosenLootList)
        {
            DontDestroyOnLoad(loot);
            decorationsList.Add(loot);
        }
    }
}
