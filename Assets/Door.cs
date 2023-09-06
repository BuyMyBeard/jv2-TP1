using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    bool Openable = false;
    public void CollectKeys()
    {
        promptMessage = "Open door";
        Openable = true;
    }
    public override void Interact()
    {
        if (Openable)
        {
            gameObject.SetActive(false);
        }
    }
}
