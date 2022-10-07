using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Image icon;

    [HideInInspector]
    public ItemSO item;

    [HideInInspector]
    public PlayerQuestManager pqm;

    [HideInInspector]
    public QuestTransactor qT;

    public void UpdateItem()
    {
        icon.sprite = item.itemSprite;
        qT.items.Add(gameObject);
    }

    public void RemoveItem()
    {
        pqm.questItems.Remove(item);
        qT.items.Remove(gameObject);
        Destroy(gameObject);
    }
}
