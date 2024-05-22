using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public abstract class UserInterface : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;
    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    [SerializeField] GameObject boxPrefab;
    [SerializeField] GameObject environment;
    [SerializeField] Transform faceTransform;
    public abstract void CreateSlots();

    private void Update()
    {
        slotsOnInterface.UpdateSlotDisplay();
        //Debug.Log(MouseData.slotId);
    }
    private void OnEnable()
    {
        for (int i = 0; i < inventory.Container.Slots.Length; i++)
        {
            inventory.Container.Slots[i].parent = this;
            inventory.Container.Slots[i].parentId = 0;
        }
        for (int i = 0; i < equipment.Container.Slots.Length; i++)
        {
            equipment.Container.Slots[i].parent = this;
            equipment.Container.Slots[i].parentId = 1;
        }
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }
    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (!trigger) 
        { 
            Debug.LogWarning("No EventTrigger component found!");
            return;
        }
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
        if(MouseData.interfaceMouseIsOver is StaticInterface)
        {
            MouseData.slotInterfacetID = equipment.id;
        }
        else if(MouseData.interfaceMouseIsOver is DynamicInterface)
        {
            MouseData.slotInterfacetID = inventory.id;
        }
    }
    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
        MouseData.slotInterfacetID = -1;
    }
    public void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver=obj;
    }
    public void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
    }
    public void OnDragStart(GameObject obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
    }
    private GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        if (slotsOnInterface[obj].id>=0)
        {
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            tempItem.transform.SetParent(transform.parent);

            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].item.itemObject.uiDisplay;
            img.raycastTarget = false;
        }
        return tempItem;
    }
    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemBeingDragged);
        if(MouseData.interfaceMouseIsOver==null)
        {
            Vector3 offset = new Vector3(0f, 0f, 3f);
            GameObject tempBox = Instantiate(boxPrefab, faceTransform.transform.position, Quaternion.identity, environment.transform);
            tempBox.GetComponent<GroundItem>().id = slotsOnInterface[obj].id;
            tempBox.GetComponent<GroundItem>().amount= slotsOnInterface[obj].amount;
            tempBox.GetComponent<GroundItem>().item= new Item(slotsOnInterface[obj].id, slotsOnInterface[obj].amount, slotsOnInterface[obj].item.Name, slotsOnInterface[obj].item.itemObject);
            slotsOnInterface[obj].RemoveItem();
            return;
        }
        if(MouseData.slotHoveredOver&& slotsOnInterface[obj].id>=0)
        {
            InventorySlot slot = new InventorySlot(InventoryManager.Instance.GetInventoryObject[MouseData.slotInterfacetID].type.slotsOnInterface[MouseData.slotHoveredOver].item,
                                                   InventoryManager.Instance.GetInventoryObject[MouseData.slotInterfacetID].type.slotsOnInterface[MouseData.slotHoveredOver].id,
                                                   InventoryManager.Instance.GetInventoryObject[MouseData.slotInterfacetID].type.slotsOnInterface[MouseData.slotHoveredOver].amount,
                                                   InventoryManager.Instance.GetInventoryObject[MouseData.slotInterfacetID].type.slotsOnInterface[MouseData.slotHoveredOver].parentId,
                                                   InventoryManager.Instance.GetInventoryObject[MouseData.slotInterfacetID].type.slotsOnInterface[MouseData.slotHoveredOver].slotId
                                                   ); /*MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];*/

            InventoryManager.Instance.SwapSlots(slotsOnInterface[obj], slot);
            slotsOnInterface.UpdateSlotDisplay();
        }
    }
    public void OnDrag(GameObject obj)
    {
        if(MouseData.tempItemBeingDragged!=null)
        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position=Input.mousePosition;
        }
    }
}
public static class ExtensionMethods
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject,InventorySlot> _slotOnInterface)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> slot in _slotOnInterface)
        {
            if (slot.Value.id >= 0)
            {
                slot.Key.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 255);
                slot.Key.transform.GetChild(1).GetComponent<Image>().sprite = slot.Value.item.itemObject.uiDisplay;
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
}