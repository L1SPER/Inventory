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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equipment.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
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
        inventory.Container.Items = new InventorySlot[30];
        equipment.Container.Items=new InventorySlot[7];
    }
}
