using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogClock : MonoBehaviour
{
    private void Start()
    {
        UpdateClock();
    }

    public void UpdateClock()
    {
        float hourAngle = -360 * WindingTime.S.hours / 12f;
        
        this.GetComponent<RectTransform>().localEulerAngles = new Vector3(0f, 0f, hourAngle);
    }
}
