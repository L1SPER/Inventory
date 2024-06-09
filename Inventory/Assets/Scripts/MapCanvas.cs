using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapCanvas : MonoBehaviour
{
    [Header("Map Attributes")]

    [SerializeField] private Camera cam;

    [SerializeField] float minXZoom;
    [SerializeField] float maxXZoom;
    [SerializeField] float minZZoom;
    [SerializeField] float maxZZoom;

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

    private bool isMapOpen;
    [SerializeField] private GameObject mapCanvas;
    [SerializeField] private Transform playerUi;

    private void Start()
    {
        zoom = cam.orthographicSize;
        isMapOpen = false;
    }
    private void LateUpdate()
    {
        ZoomInAndOutMap();
        MouseDrag();
        ClampBorders();
    }
    private void ClampBorders()
    {
        Vector3 tmpPos = transform.position;
        tmpPos.x = Math.Clamp(transform.position.x, minXZoom, maxXZoom);
        tmpPos.z = Math.Clamp(transform.position.z, minZZoom, maxZZoom);
        transform.position = tmpPos;
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
        if(FindObjectOfType<InventoryMouseUi>().isMapOpen)
        {
            if (Input.GetMouseButton(0))
            {
                difference = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x - cam.transform.position.x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z - cam.transform.position.z);
                if (!drag)
                {
                    drag = true;
                    origin = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, 0f, cam.ScreenToWorldPoint(Input.mousePosition).z);
                }
            }
            else
            {
                drag = false;
            }
            if (drag)
            {
                Vector3 diff = origin - difference;
                cam.transform.position = new Vector3(diff.x, 180f, diff.z);
            }
            //if (Input.GetMouseButton(1))
            //    cam.transform.position = camStartPos;
        }
    }
    public void OpenMap()
    {
        isMapOpen = true;
        mapCanvas.gameObject.SetActive(true);
        playerUi.localScale = new Vector3(15f, 15f, 15f);
    }
    public void CloseMap()
    {
        isMapOpen = false;
        mapCanvas.gameObject.SetActive(false);
        playerUi.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
