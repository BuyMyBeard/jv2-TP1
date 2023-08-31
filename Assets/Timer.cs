using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timeElapsed = 0;
    TextMeshProUGUI display;
    public int milliseconds { get => (int)(timeElapsed % 1 * 1000); }
    public int seconds { get => (int)(timeElapsed % 60); }
    public int minutes { get => (int)(timeElapsed / 60); }
    public override string ToString() => $"{minutes.ToString("D2")}:{seconds.ToString("D2")}.{milliseconds.ToString("D3")}";
    void Awake()
    {
        display = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        timeElapsed += Time.deltaTime;
        display.SetText(ToString());
    }
}
