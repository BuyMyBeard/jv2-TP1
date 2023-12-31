using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    ButtonPrompt buttonPrompt;
    public string promptMessage = "Interact";
    protected virtual void Awake()
    {
        buttonPrompt = FindObjectOfType<ButtonPrompt>();
    }
    protected void Prompt()
    {
        buttonPrompt.ProposePrompt(this);
    }
    protected void CancelPrompt()
    {
        buttonPrompt.CancelPrompt(this);
    }    
    public abstract void Interact();
    protected virtual void OnTriggerEnter(Collider other)
    {
        Prompt();
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        CancelPrompt();
    }
}
public class ButtonPrompt : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textPrompt;
    [SerializeField] GameObject[] display;
    //[SerializeField] float cooldown = 0.5f;
    Transform player;
    List<Interactable> possiblePrompts = new();
    Interactable currentPrompt;
    //bool onCooldown = false;

    void Awake()
    {
        player = FindObjectOfType<PlayerMove>().transform;
        HidePrompt();
    }
    void Update()
    {
        
        if (currentPrompt != null && PlayerInputs.InteractPress && Time.timeScale != 0)
        {
            currentPrompt.Interact();
            CancelPrompt(currentPrompt);
            currentPrompt = null;
            HidePrompt();
            //StartCoroutine(Cooldown());
        }
    }

    //IEnumerator Cooldown()
    //{
    //    onCooldown = true;
    //    yield return new WaitForSeconds(cooldown);
    //    onCooldown = false;
    //}
    void LateUpdate()
    {
        if (possiblePrompts.Count == 0)
        {
            if (currentPrompt != null)
            {
                currentPrompt = null;
                HidePrompt();
            }    
            return;
        }
        if (possiblePrompts.Count == 1)
        {
            if (possiblePrompts.First() == currentPrompt)
                return;
            currentPrompt = possiblePrompts.First();
        }
        else
        {
            Interactable closestPrompt = possiblePrompts.First();
            float shortestDistance = (player.position - closestPrompt.transform.position).magnitude;
            for (int i = 1; i < possiblePrompts.Count; i++)
            {
                float distance = (player.position - possiblePrompts[i].transform.position).magnitude;
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestPrompt = possiblePrompts[i];
                }
            }
            currentPrompt = closestPrompt;
        }
        ShowPrompt(currentPrompt.promptMessage);
    }
    public void ProposePrompt(Interactable interactable)
    {
        if (!possiblePrompts.Contains(interactable))
            possiblePrompts.Add(interactable);
    }
    public void CancelPrompt(Interactable interactable)
    {
        possiblePrompts.Remove(interactable);
    }
    void HidePrompt()
    {
        foreach (var c in display) c.SetActive(false);
    }
    void ShowPrompt(string message)
    {
        textPrompt.SetText(message);
        foreach (var c in display) c.SetActive(true);
    }
    
}
