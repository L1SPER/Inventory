using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface
{
    public GameObject[] slots;
    private void Start()
    {
        equipment.type = this;
        CreateSlots();
    }
    public override void CreateSlots()
    {
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            InventorySlot slot = equipment.GetSlots[i];
            
            var obj= slots[i];
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            slotsOnInterface.Add(obj, slot);
        }
    }
}
