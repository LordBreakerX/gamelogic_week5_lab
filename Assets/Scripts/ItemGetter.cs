using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGetter : MonoBehaviour
{
    public Image icon;

    [HideInInspector]
    public ItemSO item;

    [HideInInspector]
    public GameObject inventoryContent;

    [HideInInspector]
    public GameObject itemPrefab;

    [HideInInspector]
    public PlayerQuestManager pqm;

    [HideInInspector]
    public QuestTransactor qT;


    public void UpdateItem()
    {
        icon.sprite = item.itemSprite;
    }

    public void AddItem()
    {
        pqm.questItems.Add(item);

        GameObject questItem = Instantiate(itemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        questItem.transform.SetParent(inventoryContent.transform, false);
        Item itemScript = questItem.GetComponent<Item>();
        itemScript.item = item;
        itemScript.pqm = pqm;
        itemScript.qT = qT;
        itemScript.UpdateItem();

        if (pqm.activeQuest != null)
        {
         qT.CountQuestItems();
        }
    }

}
