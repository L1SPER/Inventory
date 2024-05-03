using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject itemSlotPrefab;

    public InventoryObject inventory;
    Dictionary<GameObject,InventorySlot> slotsOnInterface= new Dictionary<GameObject,InventorySlot>();

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
        foreach (KeyValuePair<GameObject,InventorySlot> slot in slotsOnInterface)
        {
            if(slot.Value.id>=0)
            {
                //slot.Key.transform.GetChild(0).GetComponent<Image>().color = new Color(212, 217, 162, 255);
                slot.Key.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 255);
                slot.Key.transform.GetChild(1).GetComponent<Image>().sprite = slot.Value.item.itemObject.uiDisplay;
                slot.Key.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = slot.Value.amount.ToString();
            }
            else
            {
                //slot.Key.transform.GetChild(0).GetComponent<Image>().color = new Color(212, 217, 162, 255);
                slot.Key.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 0);
                slot.Key.transform.GetChild(1).GetComponent<Image>().sprite = null;
                slot.Key.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    private void CreateDisplay()
    {
        for (int i = 0; i < inventory.inventory.Items.Length; i++)
        {
            var obj = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            //obj.transform.GetChild(0).GetComponent<Image>().color = new Color(212, 217, 162, 255);
            obj.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 0);
            obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

            slotsOnInterface.Add(obj, inventory.inventory.Items[i]);
        }
    }
    private Vector3 GetPosition(int index)
    {
        return new Vector3(xStartPos + (xSpaceBetweenItem * (index % numberOfColumn)), yStartPos - (ySpaceBetweenItem * (index / numberOfRow)), 0f);
    }
}
