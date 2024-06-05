using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Transform player;
    [Header("Map Attributes")]

    [SerializeField] private Camera cam;

    [SerializeField] private float zoomMultiplier = 4f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 8f;
    [SerializeField] private float velocity = 0f;
    [SerializeField] private float smoothTime = 0.25f;

    private Vector3 origin;
    private Vector3 difference;
    public Vector3 camStartPos;

    private bool drag = false;
    private float zoom;

    private void Start()
    {
        zoom = cam.orthographicSize;
    }
    private void LateUpdate()
    {
        ZoomInAndOutMap();
        MouseDrag();
    }
    private void ZoomInAndOutMap()
    {
        if (FindObjectOfType<InventoryMouseUi>().isMapOpen)
        {
            Debug.Log("1234");
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            zoom -= scroll * zoomMultiplier;
            zoom = Math.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
        }
    }
    private void MouseDrag()
    {
        if (Input.GetMouseButton(0))
        {
            difference = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x - cam.transform.position.x, 0f,cam.ScreenToWorldPoint(Input.mousePosition).z - cam.transform.position.z);
            if (!drag)
            {
                drag = true;
                origin = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x,0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
            }
        }
        else
        {
            drag = false;
        }
        if (drag)
        {
            Vector3 diff = origin - difference;
            cam.transform.position =new Vector3(diff.x,180f,diff.z) ;
        }
        if (Input.GetMouseButton(1))
            cam.transform.position = camStartPos;
    }
}
