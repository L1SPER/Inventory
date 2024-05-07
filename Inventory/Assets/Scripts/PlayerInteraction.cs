using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera fpsCam;
    [SerializeField]
    private float range;
    public InventoryObject inventory;
    public readonly KeyCode inventoryKey = KeyCode.Tab;
    [SerializeField] private GameObject canvas;
    private bool isInventoryOpen;

    private void Start()
    {
        isInventoryOpen = false;
    }

    private void Update()
    {
        OpenInventory();
        Shoot();
        SaveLoadFunctions();
    }
    private void SaveLoadFunctions()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
        }
    }

    private void OpenInventory()
    {
        if(Input.GetKeyDown(inventoryKey)&&!isInventoryOpen)
        {
            canvas.gameObject.SetActive(true);
            isInventoryOpen= true;
        }
        else if(Input.GetKeyDown(inventoryKey)&&isInventoryOpen)
        {
            canvas.gameObject.SetActive(false);
            isInventoryOpen = false;
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Item item=hit.transform.GetComponent<Item>();
            IInteractable InteractableObj = hit.transform.GetComponent<IInteractable>();
            if (InteractableObj != null)
            {
                InteractableObj.InteractWithoutPressingButton();
                if (Input.GetButtonDown("Fire1")&&item)
                {
                    inventory.AddItem(item);
                }
                else if(Input.GetButtonDown("Fire1"))
                {
                    InteractableObj.InteractWithPressingButton();
                }
            }
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[30];
    }
}
