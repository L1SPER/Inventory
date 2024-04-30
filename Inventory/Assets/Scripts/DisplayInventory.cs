using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField]
    private float xStartPos;
    [SerializeField]
    private float yStartPos;
    [SerializeField]
    private float xSpaceBetweenItem;
    [SerializeField]
    private float ySpaceBetweenItem;
    [SerializeField]
    private int numberOfColumn;
    [SerializeField]
    private int numberOfRow;

    [SerializeField]
    private GameObject itemSlot;
    

    private void Start()
    {
        CreateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        //Envanter anlýk güncellenecek.
    }

    private void CreateDisplay()
    {
        for (int i = 0; i < (numberOfColumn*numberOfRow); i++)
        {
            var obj = Instantiate(itemSlot, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        }
    }
    private Vector3 GetPosition(int index)
    {
        return new Vector3(xStartPos + (xSpaceBetweenItem * (index % numberOfColumn)), yStartPos - (ySpaceBetweenItem * (index / numberOfRow)), 0f);
    }
}
