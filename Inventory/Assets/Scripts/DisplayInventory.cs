using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class DisplayInventory : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();

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
    Dictionary<GameObject,InventorySlot> slotsOnInterface=new Dictionary<GameObject,InventorySlot>();

    private void Start()
    {
        CreateSlots();
    }
    private void Update()
    {
        UpdateSlots();
    }
    private void CreateSlots()
    {
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];

            var obj = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 0);
            obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            slotsOnInterface.Add(obj, slot);
        }
    }
    private void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> slot in slotsOnInterface)
        {
            if(slot.Value.id>=0)
            {
                slot.Key.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 255);
                slot.Key.transform.GetChild(1).GetComponent<Image>().sprite = inventory.database.GetItem[slot.Value.item.id].uiDisplay;
                slot.Key.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = slot.Value.amount.ToString();
            }
            else
            {
                slot.Key.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 0);
                slot.Key.transform.GetChild(1).GetComponent<Image>().sprite = null;
                slot.Key.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
    private void AddEvent(GameObject obj,EventTriggerType type,UnityAction<BaseEventData> action)
    {
        EventTrigger trigger=obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnEnter(GameObject obj)
    {
        mouseItem.hoverObj = obj;
        if(slotsOnInterface.ContainsKey(obj))
        {
            mouseItem.hoverItem = slotsOnInterface[obj];
        }
    }
    public void OnExit(GameObject obj)
    {
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
    }
    public void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt=mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        if (slotsOnInterface[obj].id>=0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[slotsOnInterface[obj].id].uiDisplay;
            img.raycastTarget = false;
        }
        mouseItem.obj = mouseObject;
        mouseItem.item = slotsOnInterface[obj];
    }
    public void OnDragEnd(GameObject obj)
    {
        if (mouseItem.hoverObj)
        {
            inventory.SwapItem(slotsOnInterface[obj],mouseItem.hoverItem);
        }
        else
        {
            slotsOnInterface[obj].RemoveItem();
        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }
    public void OnDrag(GameObject obj)
    {
        if(mouseItem.obj != null)
        {
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
    private Vector3 GetPosition(int index)
    {
        return new Vector3(xStartPos + (xSpaceBetweenItem * (index % numberOfColumn)), yStartPos - (ySpaceBetweenItem * (index / numberOfRow)), 0f);
    }
}
public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}
