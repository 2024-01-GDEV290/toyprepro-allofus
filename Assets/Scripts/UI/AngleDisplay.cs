using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AngleDisplay : MonoBehaviour
{
    private TextMeshProUGUI angleText;

    private void Awake()
    {
        angleText = GetComponent<TextMeshProUGUI>();
        angleText.text = "Current angle: 0";
    }

    public void UpdateAngle()
    {
        string angle = "Current angle: " + WindingTime.S.degrees.ToString();
        angleText.text = angle;
    }
}
