using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AngleDisplay : MonoBehaviour
{
    private TextMeshProUGUI clockDisplay;

    private void Awake()
    {
        clockDisplay = GetComponent<TextMeshProUGUI>();
        clockDisplay.text = "Current angle: 0";
    }

    public void UpdateAngle()
    {
        string angle = "Current angle: "+ WindingTime.S.degrees.ToString();
        clockDisplay.text = angle;
    }
}
