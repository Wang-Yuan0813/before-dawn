using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    [TextArea]
    public string itemInfo;

    public bool isEmpty;
}
