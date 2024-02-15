using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindingTime : MonoBehaviour
{
    private int _degrees;
    private static int maxDegrees = 360;
    public static WindingTime S;

    public int degrees
    {
        get { return _degrees; }
    }

    public int hours
    {
        get
        {
            return _degrees / 15;
        }
    }

    public int minutes
    {
        get
        {
            return (_degrees % 15) * 4;
        }
    }

    private void Awake()
    {
        _degrees = 0;
        if (S == null) { S = this; }
    }

    public void AdvanceTime(int steps)
    {
        _degrees += steps;
        if (_degrees > maxDegrees)
        {
            _degrees -= maxDegrees;
        }
    }

    public void RewindTime(int steps)
    {
        _degrees -= steps;
        if (_degrees < 0)
        {
            _degrees += maxDegrees;
        }
    }

    public string ClockTime()
    {
        string displayTime = hours.ToString() + ":";
        if (minutes < 10) { displayTime += "0"; }
        displayTime += minutes.ToString();
        return displayTime;
    }
}
