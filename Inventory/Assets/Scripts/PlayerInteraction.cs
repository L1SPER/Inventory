using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float fpsCamRange;
    private float mapCamRange;
    public InventoryObject inventory;
    public InventoryObject equipment;
    
    public readonly KeyCode inventoryKey = KeyCode.I;
    public readonly KeyCode mapKey = KeyCode.M;

    [Header("Cams")]
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Camera mapCam;

    private bool isInventoryOpen;
    private bool isMapOpen;
    private bool canHit;

    private IInteractable lastInteractableObj;

    private void Start()
    {
        lastInteractableObj = null;
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
        FindObjectOfType<InventoryMouseUi>().isInventoryOpen = true;
        GetComponentInChildren<FPSCameraController>().canRotate = false;
        FindObjectOfType<InventoryCanvas>().OpenInventory();
    }
    private void CloseInventory()
    {
        canHit = true;
        isInventoryOpen = false;
        FindObjectOfType<InventoryMouseUi>().isInventoryOpen = false;
        GetComponentInChildren<FPSCameraController>().canRotate = true;
        FindObjectOfType<InventoryCanvas>().CloseInventory();
    }
    public void OpenMap()
    {
        canHit = false;
        isMapOpen = true;
        FindObjectOfType<InventoryMouseUi>().isMapOpen = true;
        FindObjectOfType<MapCanvas>().camStartPos = transform.position;
        GetComponentInChildren<FPSCameraController>().canRotate = false;
        FindObjectOfType<MapCanvas>().OpenMap();
    }
    private void CloseMap()
    {
        canHit = true;
        isMapOpen = false;
        FindObjectOfType<InventoryMouseUi>().isMapOpen=false;
        GetComponentInChildren<FPSCameraController>().canRotate = true;
        FindObjectOfType<MapCanvas>().CloseMap();
    }
    private void Shoot()
    {
        RaycastHit hit;
        Transform transform;
        float _range;
        if(FindObjectOfType<InventoryMouseUi>().isMapOpen)
        {
            _range = mapCamRange;
            transform = mapCam.transform;
        }
        else
        {
            _range = fpsCamRange;
            transform = fpsCam.transform; 
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, _range))
        {
            IInteractable InteractableObj = hit.transform.GetComponent<IInteractable>();
            if (InteractableObj != null && canHit)
            {
                if (InteractableObj != lastInteractableObj)
                {
                    if (lastInteractableObj != null)
                    {
                        lastInteractableObj.InteractWithoutPressingButton(false);
                    }
                }
                lastInteractableObj = InteractableObj;
                InteractableObj.InteractWithoutPressingButton(true);
                if (Input.GetButtonDown("Fire1"))
                {
                    InteractableObj.InteractWithPressingButton();
                    lastInteractableObj = null;
                }
            }
        }
        else
        {
            if (lastInteractableObj != null)
            {
                lastInteractableObj.InteractWithoutPressingButton(false);
                lastInteractableObj = null;
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
    }
}
