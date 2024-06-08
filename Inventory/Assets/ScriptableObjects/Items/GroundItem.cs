using System;
using UnityEngine;

public class GroundItem : MonoBehaviour, IInteractable
{
    public Item item;
    public int id;
    public int amount;
    public string Name;
    private void Start()
    {
        item = new Item(item.itemObject.id, amount, item.itemObject);
        this.id = item.id;
        this.Name = item.Name;
        this.gameObject.name = this.Name;
    }
    public void InteractWithoutPressingButton()
    {
    }
    public void InteractWithoutPressingButton(bool _isInteracting)
    {
        GetComponent<ItemInfo>().OnInteract(_isInteracting);
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
