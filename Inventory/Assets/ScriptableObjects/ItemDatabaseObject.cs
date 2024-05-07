using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemDatabaseObject",menuName = "ItemDatabaseObject")]
public class ItemDatabaseObject : ScriptableObject,ISerializationCallbackReceiver
{
    public ItemObject [] items;
    Dictionary<ItemObject, int> GetItem=new Dictionary<ItemObject, int>();

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].id = i;
            if (!items[i].isStackable)
            {
                items[i].slotAmountMax = 1;
            }
            GetItem.Add(items[i], i);
        }
    }
    public void OnBeforeSerialize()
    {
        GetItem=new Dictionary<ItemObject, int>();
    }
}
