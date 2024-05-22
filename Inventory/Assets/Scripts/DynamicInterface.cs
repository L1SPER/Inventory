using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicInterface : UserInterface
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
    private int numberOfRow;
   
    [SerializeField]
    private GameObject itemSlotPrefab;

    private void Start()
    {
        inventory.type = this;
        CreateSlots();
    }
    public override void CreateSlots()
    {
        for (int i = 0; i < inventory.Container.Slots.Length; i++)
        {
            InventorySlot slot = inventory.GetSlots[i];

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
    private Vector3 GetPosition(int index)
    {
        return new Vector3(xStartPos + (xSpaceBetweenItem * (index % numberOfRow)), yStartPos -(ySpaceBetweenItem * (index / numberOfRow)), 0f);
    }
}