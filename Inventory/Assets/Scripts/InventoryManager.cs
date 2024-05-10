using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour,ISerializationCallbackReceiver
{
    public InventoryObject[] inventoryObjects;
    public Dictionary<int, InventoryObject> GetInventoryObject=new Dictionary<int, InventoryObject>();
    public static InventoryManager Instance { get; private set; }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < inventoryObjects.Length; i++)
        {
            inventoryObjects[i].id = i;
            GetInventoryObject.Add(i, inventoryObjects[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        GetInventoryObject = new Dictionary<int, InventoryObject>();
    }

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            DontDestroyOnLoad(Instance);
        }
    }
}
