using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour,IInteractable
{
    private bool isActive;
    public int id;
    private Vector3 destination;
    [SerializeField] string Name;
    
    void Start()
    {
        destination= transform.position;
        isActive = false;
        Name = "Teleport";
    }
    private void OnInteract()
    {
        if(!isActive)
            isActive= true;

        FindObjectOfType<PlayerInteraction>().OpenMap();
    }
    public void InteractWithoutPressingButton()
    {

    }
    public void InteractWithoutPressingButton(bool _isInteracting)
    {
        GetComponent<ObjectInfo>().OnInteract(_isInteracting);
    }
    public void InteractWithPressingButton()
    {
        OnInteract();
    }
    public void ButtonInteraction()
    {
        Debug.Log("1");
    }
}
