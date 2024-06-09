using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvas : MonoBehaviour
{
    [SerializeField] private GameObject inventoryCanvas;
    private bool isInventoryOpen;
    private void Start()
    {
        isInventoryOpen = false;
    }
    public void OpenInventory()
    {
        isInventoryOpen = true;
        inventoryCanvas.gameObject.SetActive(true);
    }
    public void CloseInventory()
    {
        isInventoryOpen = false;
        inventoryCanvas.gameObject.SetActive(false);
    }
}
