using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject itemObject;
    public int id;
    public int amount;
    private void Awake()
    {
        this.id = itemObject.id;
    }
}
