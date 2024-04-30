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
            Debug.Log(hit.transform.name);
            ItemInfo itemInfo = hit.transform.GetComponent<ItemInfo>();
            if (itemInfo != null)
            {
                itemInfo.StartCoroutine("OpenCanvasByTime");
                if (Input.GetButtonDown("Fire1"))
                {
                    //Envantere alýncak.
                }

            }
        }
    }
}
