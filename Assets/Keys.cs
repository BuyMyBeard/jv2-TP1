using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : Interactable
{
    public override void Interact()
    {
        FindObjectOfType<Door>().CollectKeys();
        gameObject.SetActive(false);
    }
}
