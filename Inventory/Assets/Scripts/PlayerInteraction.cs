using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera fpsCam;
    [SerializeField]
    private float range;
    public InventoryObject slot;

    private void Update()
    {
        Shoot();
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
