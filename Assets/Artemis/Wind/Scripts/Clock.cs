using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private TextMeshProUGUI clockDisplay;

    private void Awake()
    {
        clockDisplay = GetComponent<TextMeshProUGUI>();
        clockDisplay.text = "0:00";
    }

    public void UpdateTime()
    {
        string time = WindingTime.S.ClockTime();
        clockDisplay.text = time;
    }
}
