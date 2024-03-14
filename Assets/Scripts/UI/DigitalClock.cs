using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DigitalClock : MonoBehaviour
{
    private TextMeshProUGUI clockDisplay;

    private void Awake()
    {
        clockDisplay = GetComponent<TextMeshProUGUI>();
        clockDisplay.text = "Time: 0:00";
    }

    public void UpdateTime()
    {
        string time = "Time: " + WindingTime.S.ClockTime();
        clockDisplay.text = time;
    }
}
