using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera fpsCam;
    [SerializeField]
    private float range;
    public InventoryObject slot;
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
            Debug.Log(hit.transform.name);
            IInteractable InteractableObj = hit.transform.GetComponent<IInteractable>();
            if (InteractableObj != null)
            {
                InteractableObj.InteractWithoutPressingButton();
                if (Input.GetButtonDown("Fire1")&&item)
                {
                    slot.AddItem(item);
                }
                else if(Input.GetButtonDown("Fire1"))
                {
                    InteractableObj.InteractWithPressingButton();
                }
            }
        }
    }
}
