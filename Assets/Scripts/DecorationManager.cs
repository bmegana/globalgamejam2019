using UnityEngine;
using UnityEngine.UI;

public class DecorationManager : MonoBehaviour
{
    public static DecorationManager instance;
    public GameObject[] decorationsList; //TODO: integrate with looting script

    public GameObject decorationsPanel;
    public GameObject decorations;

    public GameObject decorationOptionsPanel;

    public GameObject inGameDecorImages;

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
        if (loot != null)
        {
            if (decorationsList[decorationIndexToEdit] != null)
            {
                Destroy(decorationsList[decorationIndexToEdit]);
            }
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
    }

    public void UpdateInGameDecorDisplay()
    {
        int childCount = inGameDecorImages.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject decPanelChild =
                decorations.transform.GetChild(i).gameObject;
            DecorationSlotBehavior decSlotBehavior =
                decPanelChild.GetComponent<DecorationSlotBehavior>();
            Sprite decSlotSprite = decSlotBehavior.getSprite();

            GameObject inGameDecChild =
                inGameDecorImages.transform.GetChild(i).gameObject;
            InGameDecorDisplay displayScript =
                inGameDecChild.GetComponent<InGameDecorDisplay>();
            displayScript.UpdateDisplay(decSlotSprite);
        }
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
