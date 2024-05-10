using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemDatabaseObject",menuName = "ItemDatabaseObject")]
public class ItemDatabaseObject : ScriptableObject,ISerializationCallbackReceiver
{
    public ItemObject [] items;
    public Dictionary< int, ItemObject> GetItem=new Dictionary<int, ItemObject>();

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].id = i;
            if (!items[i].isStackable)
            {
                items[i].slotAmountMax = 1;
            }
            GetItem.Add(i, items[i]);
        }
    }
    public void OnBeforeSerialize()
    {
        GetItem=new Dictionary< int, ItemObject>();
    }
}
