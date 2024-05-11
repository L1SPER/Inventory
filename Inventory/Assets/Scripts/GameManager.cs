using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    private static GameManager gameManager;
    public static GameManager Instance{ get=>gameManager; set=>gameManager=value; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DontDestroyOnLoad(Instance);
        }
    }
}
