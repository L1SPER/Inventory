using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera[] cameras;
    private static CameraManager cameraManager;
    public static CameraManager Instance { get => cameraManager; set => cameraManager = value; }
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
    public void ChangeCamera(Camera before,Camera after)
    {
        foreach (Camera camera in cameras)
        {
            if(before==camera)
            {
                camera.enabled= false;
            }
            if(after==camera)
            {
                camera.enabled= true;
            }
        }
    }
}
