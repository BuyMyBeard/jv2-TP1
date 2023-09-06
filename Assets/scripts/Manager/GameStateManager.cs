using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    GameObject loseMenu;
    TextMeshProUGUI score;
    void Awake()
    {
        loseMenu = GameObject.FindGameObjectWithTag("LoseScreen");
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        loseMenu.SetActive(false);
        ResumeGame();
    }
    public void Lose(string message = "The monster caught you! \nYou lasted ")
    {
        score.text = message + FindObjectOfType<Timer>().ToString();
        loseMenu.SetActive(true);
        PauseGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

}
