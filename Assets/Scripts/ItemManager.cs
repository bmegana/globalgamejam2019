using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public GameObject[] decorationsList; //TODO: integrate with looting script
    public int decListSize = 15;

    public GameObject decorationsPanel;
    public GameObject decorations;

    public GameObject decorationOptionsPanel;

    private int decorationIndexToEdit;

    public GameObject dummyLootPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        decorationsList = new GameObject[decListSize];
    }

    public void DeactivateAllPanels()
    {
        decorationsPanel.SetActive(false);
        decorationOptionsPanel.SetActive(false);
        dummyLootPanel.SetActive(false);
    }

    public void ActivateDecoratePanel()
    {
        decorationsPanel.SetActive(true);
    }

    public void DeactivateDecoratePanel()
    {
        decorationsPanel.SetActive(false);
    }

    public void ActivateDecorationOptionsPanel()
    {
        decorationOptionsPanel.SetActive(true);
    }

    public void DeactivateDecorationOptionsPanel()
    {
        decorationOptionsPanel.SetActive(false);
    }

    public void ActivateDummyLootPanel()
    {
        dummyLootPanel.SetActive(true);
    }

    public void DeactivateDummyLootPanel()
    {
        dummyLootPanel.SetActive(false);
    }

    public void SetDecorationIndexToEdit(int index)
    {
        decorationIndexToEdit = index;
    }

    public void UpdateDecoration(GameObject loot)
    {
        DontDestroyOnLoad(loot);
        decorationsList[decorationIndexToEdit] = loot;
        DeactivateDecorationOptionsPanel();
        DeactivateDummyLootPanel();

        GameObject decorSlot =
            decorations.transform.GetChild(decorationIndexToEdit).gameObject;
        DecorationSlotBehavior dsb =
            decorSlot.GetComponent<DecorationSlotBehavior>();
        dsb.UpdateSlot(loot.GetComponent<SpriteRenderer>().sprite);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (decorationsPanel.activeSelf) DeactivateDecoratePanel();
            else ActivateDecoratePanel();
        }
    }
}
