using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemPopulator : MonoBehaviour
{

    public List<ItemSO> pickupItems;

    public GameObject inventoryContent;
    public GameObject pickupContent;

    public GameObject itemPrefab;
    public GameObject itemGetterPrefab;

    public PlayerQuestManager pqm;

    public QuestTransactor questTransactor;

    private void Start()
    {
        foreach(ItemSO item in pickupItems)
        {
            GameObject itemGetter = Instantiate(itemGetterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            itemGetter.transform.SetParent(pickupContent.transform, false);

            ItemGetter itemGetterScript = itemGetter.GetComponent<ItemGetter>();
            itemGetterScript.itemPrefab = itemPrefab;
            itemGetterScript.pqm = pqm;
            itemGetterScript.inventoryContent = inventoryContent;
            itemGetterScript.item = item;
            itemGetterScript.qT = questTransactor;
           itemGetterScript.UpdateItem();
        }
    }


}
