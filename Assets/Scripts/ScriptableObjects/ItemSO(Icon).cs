using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (menuName = "Item/Create Item", fileName ="Item_", order = 1)] //
public class ItemSO : ScriptableObject // It is used to Optimizing Data that is Theoreticaly Singular or holds the data.
{
    public string itemName;
    public int intemValue;
    public Sprite itemSprite;
}
