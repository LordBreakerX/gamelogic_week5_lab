using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestStatus { NotAvailable, Available, InProgress, Completed, Failed }

public class QuestTransactor : MonoBehaviour
{
    public PlayerQuestManager pqm;
    public NPCQuestManager npcQuestManager;

    [HideInInspector]
    public List<GameObject> items;

    public void RequestQuest()
    {
        if (pqm.activeQuest == null)
        {
            pqm.activeQuest = npcQuestManager.offeredQuest;
            pqm.questStatus = QuestStatus.InProgress;

            Debug.Log("<color=green>Quest '" +
                pqm.activeQuest.questName + "' started.</color>");
            CountQuestItems();
        }
        else
        {
            if (pqm.questStatus == QuestStatus.Completed)
            {
                pqm.questItems.AddRange(pqm.activeQuest.rewardItems);

                int itemsLeftToRemove = pqm.activeQuest.objectiveQuantity;

                for (int x = 0; x < pqm.questItems.Count; )
                {
                    if (itemsLeftToRemove > 0 && pqm.questItems[x] == pqm.activeQuest.objectiveItem)
                    {
                        for (int i = 0; i < items.Count; )
                        {
                            if (items[i].GetComponent<Item>().item == pqm.questItems[x])
                            {
                                Destroy(items[i]);
                            }
                            ++i;
                        }

                        pqm.questItems.Remove(pqm.questItems[x]);
                        itemsLeftToRemove--;
                    }
                    ++x;
                }

                pqm.activeQuest = null;
                pqm.questStatus = QuestStatus.NotAvailable;

                Debug.Log("<color=yellow>Quest Completed!</color>");
            }
            else
            {
                Debug.Log("<color=red>Finish your active quest first!</color>");
            }
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
            Debug.Log("<color=blue>Quest objective met.</color>");
        }
    }
}
