using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportManager : MonoBehaviour,ISerializationCallbackReceiver
{
    public Teleport[] teleports;
    
    public Dictionary<int, Teleport> GetTeleports = new Dictionary<int, Teleport>();
    private static TeleportManager teleportManager;
    public static TeleportManager Instance { get => teleportManager; set => teleportManager = value; }

    [SerializeField] private GameObject canvas;

    public void Teleport()
    {

    }
    public void OnBeforeSerialize()
    {
        GetTeleports = new Dictionary<int, Teleport>();
    }
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < teleports.Length; i++)
        {
            teleports[i].id = i;
            GetTeleports.Add(i, teleports[i]);
        }
    }
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
