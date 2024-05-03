using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour,IInteractable
{
    public ItemObject itemObject;
    public int id;
    public int amount;

    public void InteractWithoutPressingButton()
    {
        GetComponent<ItemInfo>().StartCoroutine("OpenCanvasByTime");
    }

    public void InteractWithPressingButton()
    {
        
    }

    private void Awake()
    {
        this.id = itemObject.id;
    }
}
