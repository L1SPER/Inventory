using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMouseUi : MonoBehaviour
{
    [SerializeField] GameObject mouseCursor;
    public bool isInventoryOpen;
    private void Start()
    {
        isInventoryOpen = false;
    }
    private void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        if(isInventoryOpen)
        {
            Cursor.lockState = CursorLockMode.Confined;
            mouseCursor.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            mouseCursor.SetActive(true);
        }
    }
}
