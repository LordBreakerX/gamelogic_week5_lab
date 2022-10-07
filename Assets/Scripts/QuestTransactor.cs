using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum QuestStatus { NotAvailable, Available, InProgress, Completed, Failed }

public class QuestTransactor : MonoBehaviour
{
    public PlayerQuestManager pqm;
    public NPCQuestManager npcQuestManager;

    [HideInInspector]
    public List<GameObject> items;

    public GameObject inventoryContent;

    public GameObject itemPrefab;

    public TMP_Text questNameText;
    public TMP_Text questDescriptionText;
    public TMP_Text questStatusText;

    public void RequestQuest()
    {
        if (pqm.activeQuest == null)
        {
            pqm.activeQuest = npcQuestManager.offeredQuest;
            pqm.questStatus = QuestStatus.InProgress;
            UpdateQuestInfo();

            Debug.Log("<color=green>Quest '" +
                pqm.activeQuest.questName + "' started.</color>");
            CountQuestItems();
        }
        else
        {
            if (pqm.questStatus == QuestStatus.Completed)
            {
                pqm.questItems.AddRange(pqm.activeQuest.rewardItems);
                    foreach (ItemSO item in pqm.activeQuest.rewardItems)
                    {
                        pqm.questItems.Add(item);
                        GameObject reward = Instantiate(itemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        reward.transform.SetParent(inventoryContent.transform, false);
                        Item rewardScript = reward.GetComponent<Item>();
                        rewardScript.item = item;
                        rewardScript.pqm = pqm;
                        rewardScript.qT = this;
                        rewardScript.UpdateItem();
                        items.Add(reward);
                    }

                int itemsLeftToRemove = pqm.activeQuest.objectiveQuantity;


                List<GameObject> removeObject = new List<GameObject>();

                for (int x = 0; x < pqm.questItems.Count; )
                {
                    if (itemsLeftToRemove > 0 && pqm.questItems[x] == pqm.activeQuest.objectiveItem)
                    {
                        removeObject.Add(items[x]);
                        print(items[x]);

                        itemsLeftToRemove--;
                    }
                    x++;
                }

                for (int y = 0; y < removeObject.Count; y++)
                {
                    GameObject item = removeObject[y];
                    pqm.questItems.Remove(item.GetComponent<Item>().item);
                    Destroy(item);
                    items.Remove(removeObject[y]);
                    
                }
                removeObject.Clear();

                pqm.activeQuest = null;
                pqm.questStatus = QuestStatus.NotAvailable;
                UpdateQuestInfo();

                Debug.Log("<color=yellow>Quest Completed!</color>");
            }
            else
            {
                Debug.Log("<color=red>Finish your active quest first!</color>");
            }
        }
    }

    public void UpdateQuestInfo()
    {
        if (pqm.activeQuest != null) 
        {
            questNameText.text = " Quest: " + pqm.activeQuest.questName;
            questDescriptionText.text = " Description: " + pqm.activeQuest.questDescription;
            questStatusText.text = " Status: " + pqm.questStatus.ToString();
        }
        else
        {
            questNameText.text = " Quest: No Active Quest";
            questDescriptionText.text = " Description: Not Available";
            questStatusText.text = "Status: Not Available";
        }
    }

    public void CountQuestItems()
    {
        int itemCount = 0;
        foreach(ItemSO item in pqm.questItems)
        {
            if (item == pqm.activeQuest.objectiveItem)
            {
                itemCount += 1;
            }
        }

        if (itemCount >= pqm.activeQuest.objectiveQuantity)
        {
            pqm.questStatus = QuestStatus.Completed;
            UpdateQuestInfo();
            Debug.Log("<color=blue>Quest objective met.</color>");
        }
    }
}
