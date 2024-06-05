using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float range;
    public InventoryObject inventory;
    public InventoryObject equipment;
    
    public readonly KeyCode inventoryKey = KeyCode.I;
    public readonly KeyCode mapKey = KeyCode.M;

    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private GameObject mapCanvas;

    [Header("Cams")]
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Camera mapCam;

    private bool isInventoryOpen;
    private bool isMapOpen;
    private bool canHit;

    private void Start()
    {
        isInventoryOpen = false;
        isMapOpen= false;
        canHit = true;
    }
    private void Update()
    {
        Shoot();
        InventoryFunctions();
        SaveLoadFunctions();
    }
    private void SaveLoadFunctions()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& isInventoryOpen&&!isMapOpen)
        {
            inventory.Save();
            equipment.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter)&& isInventoryOpen&&!isMapOpen)
        {
            inventory.Load();
            equipment.Load();
        }
    }
    private void InventoryFunctions()
    {
        if(Input.GetKeyDown(inventoryKey)&&!isInventoryOpen)
        {
            CloseMap();
            OpenInventory();
        }
        else if(Input.GetKeyDown(inventoryKey)&&isInventoryOpen)
        {
            CloseInventory();
        }
        else if(Input.GetKeyDown(mapKey)&&!isMapOpen)
        {
            CloseInventory();
            OpenMap();
        }
        else if(Input.GetKeyDown(mapKey)&&isMapOpen)
        {
            CloseMap();
        }
    }
    private void OpenInventory()
    {
        canHit = false;
        isInventoryOpen = true;
        inventoryCanvas.gameObject.SetActive(true);
        FindObjectOfType<InventoryMouseUi>().isInventoryOpen = true;
        GetComponentInChildren<FPSCameraController>().canRotate = false;
    }
    private void CloseInventory()
    {
        canHit = true;
        isInventoryOpen = false;
        inventoryCanvas.gameObject.SetActive(false);
        FindObjectOfType<InventoryMouseUi>().isInventoryOpen = false;
        GetComponentInChildren<FPSCameraController>().canRotate = true;
    }
    private void OpenMap()
    {
        isMapOpen= true;
        mapCanvas.gameObject.SetActive(true);
        FindObjectOfType<InventoryMouseUi>().isMapOpen = true;
        FindObjectOfType<Map>().camStartPos = mapCam.transform.position;
        GetComponentInChildren<FPSCameraController>().canRotate = false;
    }
    private void CloseMap()
    {
        isMapOpen = false;
        mapCanvas.gameObject.SetActive(false);
        FindObjectOfType<InventoryMouseUi>().isMapOpen=false;
        GetComponentInChildren<FPSCameraController>().canRotate = true;
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
