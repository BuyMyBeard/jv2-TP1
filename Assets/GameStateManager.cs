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
        Time.timeScale = 1;
    }
    public void Lose()
    {
        Time.timeScale = 0;
        score.text += FindObjectOfType<Timer>().ToString();
        loseMenu.SetActive(true);
    }

}
