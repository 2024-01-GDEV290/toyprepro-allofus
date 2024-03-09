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
    private bool skyChange;
    public static WindingTime S;
    public GameEventTrigger moveTime;

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
        skyChange = false;
        //RenderSettings.ambientIntensity = 0;
    }

    private void Start()
    {
        moveTime.Raise();
    }

    //private void Update()
    //{
    //    if (skyChange)
    //    {
    //        if (hours < 6 || hours > 18)
    //        {
    //            if (RenderSettings.ambientIntensity > 0)
    //            {
    //                RenderSettings.ambientIntensity -= Time.deltaTime;
    //            }
    //            else
    //            {
    //                RenderSettings.ambientIntensity = 0;
    //                skyChange = false;
    //            }
    //        }
    //        else
    //        {
    //            float timeFromNoon = Mathf.Abs(12 - hours);
    //            float intensityGoal = (6 - timeFromNoon) / 6;
    //            float intensityShift = Time.deltaTime * (1 + intensityGoal);

    //            if (RenderSettings.ambientIntensity > intensityGoal)
    //            {
    //                RenderSettings.ambientIntensity -= intensityShift;
    //                if (RenderSettings.ambientIntensity < intensityGoal)
    //                {
    //                    RenderSettings.ambientIntensity = intensityGoal;
    //                    skyChange = false;
    //                }
    //            }
    //            else if ((RenderSettings.ambientIntensity < intensityGoal))
    //            {
    //                RenderSettings.ambientIntensity += intensityShift;
    //                if (RenderSettings.ambientIntensity < intensityGoal)
    //                {
    //                    RenderSettings .ambientIntensity = intensityGoal;
    //                    skyChange = false;
    //                }
    //            }
    //        }
    //    }

    //}

    public void AdvanceTime(int steps)
    {
        _degrees += steps;
        if (_degrees >= MAX_DEGREE)
        {
            _degrees -= MAX_DEGREE;
        }

        skyChange = true;
        moveTime.Raise();
    }

    public void RewindTime(int steps)
    {
        _degrees -= steps;
        if (_degrees < 0)
        {
            _degrees += MAX_DEGREE;
        }

        skyChange = true;
        moveTime.Raise();
    }

    public string ClockTime()
    {
        string displayTime = hours.ToString() + ":";
        if (minutes < 10) { displayTime += "0"; }
        displayTime += minutes.ToString();
        return displayTime;
    }
}
