using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera fpsCam;
    [SerializeField]
    private float range;
    public InventoryObject inventory;
    public InventoryObject equipment;

    public readonly KeyCode inventoryKey = KeyCode.Tab;
    [SerializeField] private GameObject canvas;
    private bool isInventoryOpen;
    private bool canHit;
    private void Start()
    {
        isInventoryOpen = false; 
        canHit = true;
    }

    private void Update()
    {
        InventoryFunctions();
        Shoot();
        SaveLoadFunctions();
    }
    private void SaveLoadFunctions()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& isInventoryOpen)
        {
            inventory.Save();
            equipment.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter)&& isInventoryOpen)
        {
            inventory.Load();
            equipment.Load();
        }
    }

    private void InventoryFunctions()
    {
        if(Input.GetKeyDown(inventoryKey)&&!isInventoryOpen)
        {
            OpenInventory();
        }
        else if(Input.GetKeyDown(inventoryKey)&&isInventoryOpen)
        {
            CloseInventory();
        }
    }
    private void OpenInventory()
    {
        canHit = false;
        canvas.gameObject.SetActive(true);
        isInventoryOpen = true;
        FindObjectOfType<InventoryMouseUi>().isInventoryOpen = true;
        GetComponentInChildren<CameraController>().canRotate = false;
    }
    private void CloseInventory()
    {
        canHit = true;
        canvas.gameObject.SetActive(false);
        isInventoryOpen = false;
        FindObjectOfType<InventoryMouseUi>().isInventoryOpen = false;
        GetComponentInChildren<CameraController>().canRotate = true;
    }
    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            IInteractable InteractableObj = hit.transform.GetComponent<IInteractable>();
            if (InteractableObj != null&&canHit)
            {
                InteractableObj.InteractWithoutPressingButton();
                if (Input.GetButtonDown("Fire1"))
                {
                    InteractableObj.InteractWithPressingButton();
                }
            }
        } 
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Slots = new InventorySlot[30];
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].UpdateSlot(null,-1,0, equipment.GetSlots[i].parentId, equipment.GetSlots[i].slotId, equipment.GetSlots[i].allowedItems);
        }
        //equipment.Container.Slots = new InventorySlot[7];
    }
}
