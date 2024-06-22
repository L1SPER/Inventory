using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour
{
    private GameObject canvasGameObject;

    private void Start()
    {
        CreateCanvas();
    }
    private void CreateCanvas()
    {
        CreateItemInfoCanvas();
        CreateGameObject();
    }
    private void CreateItemInfoCanvas()
    {
        canvasGameObject = new GameObject();
        canvasGameObject.AddComponent<RectTransform>();
        canvasGameObject.AddComponent<Canvas>();
        canvasGameObject.AddComponent<CanvasScaler>();
        canvasGameObject.AddComponent<GraphicRaycaster>();
        canvasGameObject.GetComponent<RectTransform>().position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        canvasGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 450);
        canvasGameObject.GetComponent<RectTransform>().localScale = new Vector3(0.006318337f, 0.006318337f, 0.006318337f);
        canvasGameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        canvasGameObject.name = "ObjectInfoCanvas";
        canvasGameObject.transform.SetParent(this.transform);
    }
    private void CreateGameObject()
    {
        GameObject textGameObject = new GameObject();
        textGameObject.name = "ObjectName";
        textGameObject.AddComponent<TextMeshProUGUI>();
        textGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.position.x, transform.position.y);
        textGameObject.GetComponent<RectTransform>().position = canvasGameObject.GetComponent<RectTransform>().position/*+new Vector3(0f, 1f, 0f)*/;
        textGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(390, 80);
        textGameObject.GetComponent<RectTransform>().localScale = new Vector3(0.006318337f, 0.006318337f, 0.006318337f);

        textGameObject.GetComponent<TextMeshProUGUI>().fontStyle = (FontStyles)FontStyle.Bold;
        textGameObject.GetComponent<TextMeshProUGUI>().fontSize = 60;
        textGameObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
        textGameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        textGameObject.GetComponent<TextMeshProUGUI>().text = name;

        textGameObject.transform.SetParent(canvasGameObject.transform);
        canvasGameObject.SetActive(false);
    }
    public void OnInteract(bool _isInteracting)
    {
        if (canvasGameObject != null)
            canvasGameObject.SetActive(_isInteracting);
        if (_isInteracting == true)
            canvasGameObject.transform.rotation = Quaternion.LookRotation(canvasGameObject.transform.position - Camera.main.transform.position);
    }
}