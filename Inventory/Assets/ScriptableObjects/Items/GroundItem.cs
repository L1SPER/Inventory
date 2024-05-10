using System;
using UnityEngine;

public class GroundItem : MonoBehaviour, IInteractable
{
    public Item item;
    public int id;
    public int amount;
    public string Name;
    private void Awake()
    {
        item = new Item(item.itemObject.id,amount,Name,item.itemObject);
        this.id = item.id;
    }
    public void InteractWithoutPressingButton()
    {
        GetComponent<ItemInfo>().StartCoroutine("OpenCanvasByTime");
    }
    public void InteractWithPressingButton()
    {
        AddToInventory();
    }
    private void AddToInventory()
    {
        InventoryManager.Instance.inventoryObjects[0].AddItem(item);
        Destroy(this.gameObject);
    }
}
