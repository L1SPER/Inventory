using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class InventoryMouseUi : MonoBehaviour
{
    [SerializeField] private GameObject mouseCursor;
    public bool isInventoryOpen;
    public bool isMapOpen;
    private void Start()
    {
        isMapOpen= false;
        isInventoryOpen = false;
    }
    private void Update()
    {
        FollowMouse();
    }
    private void FollowMouse()
    {
        if(isInventoryOpen|| isMapOpen)
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
