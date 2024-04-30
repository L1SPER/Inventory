
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    private GameObject canvasGameObject;
    public bool openCanvas;
    private void Start()
    {
        CreateCanvas();
    }

    private void CreateCanvas()
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
        canvasGameObject.name = "ItemInfoCanvas";
        canvasGameObject.transform.SetParent(this.transform);

        GameObject textGameObject = new GameObject();
        textGameObject.name = "ItemName";
        textGameObject.AddComponent<TextMeshProUGUI>();
        textGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.position.x, transform.position.y);
        textGameObject.GetComponent<RectTransform>().position = canvasGameObject.GetComponent<RectTransform>().position/*+new Vector3(0f, 1f, 0f)*/;
        textGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(390, 80);
        textGameObject.GetComponent<RectTransform>().localScale = new Vector3(0.006318337f, 0.006318337f, 0.006318337f);

        textGameObject.GetComponent<TextMeshProUGUI>().fontStyle = (FontStyles)FontStyle.Bold;
        textGameObject.GetComponent<TextMeshProUGUI>().fontSize = 60;
        textGameObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
        textGameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        textGameObject.transform.SetParent(canvasGameObject.transform);
        textGameObject.GetComponent<TextMeshProUGUI>().text = this.gameObject.name;
        //textGameObject.GetComponent<TextMeshProUGUI>().text = GetComponent<Item>().itemobject.;
        canvasGameObject.SetActive(false);

        //this.name = GetComponent<Item>().name;
    }

    public void OpenCanvas()
    {
        if (canvasGameObject != null && openCanvas)
        {
            canvasGameObject.SetActive(true);
            canvasGameObject.transform.rotation = Quaternion.LookRotation(canvasGameObject.transform.position - Camera.main.transform.position);
        }
    }

    public void CloseCanvas()
    {
        if (canvasGameObject != null && !openCanvas)
        {
            canvasGameObject.SetActive(false);
        }
    }
    public IEnumerator OpenCanvasByTime()
    {
        if (openCanvas)
            yield break;
        openCanvas = true;
        OpenCanvas();
        yield return new WaitForSeconds(1f);
        openCanvas = false;
        CloseCanvas();
    }
    public void Interact()
    {
        Debug.Log(this.gameObject.name);
    }
}
