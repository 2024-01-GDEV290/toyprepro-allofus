using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindingTime : MonoBehaviour
{
    private int _degrees;
    private const int MAX_DEGREE = 360;
    private const int DEG_PER_HR = 15;
    private const int MIN_PER_DEG = 4;
    public static WindingTime S;

    public int degrees
    {
        get { return _degrees; }
    }

    public int hours
    {
        get
        {
            return _degrees / DEG_PER_HR;
        }
    }

    public int minutes
    {
        get
        {
            return (_degrees % DEG_PER_HR) * MIN_PER_DEG;
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
        if (_degrees > MAX_DEGREE)
        {
            _degrees -= MAX_DEGREE;
        }
    }

    public void RewindTime(int steps)
    {
        _degrees -= steps;
        if (_degrees < 0)
        {
            _degrees += MAX_DEGREE;
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
