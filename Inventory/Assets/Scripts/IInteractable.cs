using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void InteractWithoutPressingButton();
    void InteractWithoutPressingButton(bool _isInteracting);
    void InteractWithPressingButton();
}
