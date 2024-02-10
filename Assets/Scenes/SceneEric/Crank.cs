using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Crank : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] GameObject cylinder;
    [SerializeField] Light dayLight;
    private float dayIntensity;
    [SerializeField] Light nightLight;
    private float nightIntensity;
    [SerializeField] float timeFlowRate = 10;
    [SerializeField] TextMeshProUGUI currentAngleDisplay;
    [SerializeField] TextMeshProUGUI currentTimeDisplay;

    [Header("Set Dynamically")]
    [SerializeField] float cylinderRotation;

    private void Awake()
    {
        cylinderRotation = cylinder.transform.localEulerAngles.y;
        dayIntensity = dayLight.intensity;
        nightIntensity = nightLight.intensity;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            RotateClockwise();
        }
        if (Input.GetKey(KeyCode.Z))
        {
            RotateCounterClockwise();
        }
        cylinderRotation = cylinder.transform.localEulerAngles.y;
        if (cylinderRotation < 0)
        {
            cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, 360 + cylinderRotation, cylinder.transform.localEulerAngles.z);
        }
        currentAngleDisplay.text = $"Current Rotation: {cylinderRotation}";
        // The 360 degrees of the cylinder's rotation = 24 hours
        // 360/24 = 15 degrees per hour
        // 1 min = 15 degrees/60 minutes = .25 degrees
        float currentHour = Mathf.Floor(cylinderRotation / 15); // Storing time in 24 hour format internally. Will eventually convert to 12 hour format for display
        float currentMinute = Mathf.Floor((cylinderRotation - currentHour * 15) / .25f);
        currentTimeDisplay.text = $"Current Time: {currentHour}:{currentMinute}";
        dayLight.intensity = CalculateArc(cylinderRotation,180) * dayIntensity;
        nightLight.intensity = CalculateArc(cylinderRotation, 0) * nightIntensity;
    }

    void RotateClockwise()
    {
        cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, cylinderRotation + timeFlowRate * Time.deltaTime,cylinder.transform.localEulerAngles.z);
    }

    void RotateCounterClockwise()
    {
        cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, cylinderRotation - timeFlowRate * Time.deltaTime, cylinder.transform.localEulerAngles.z);
    }
    float CalculateArc(float currentAngle,float brightestAngle)
    {
        float delta = Mathf.Abs(brightestAngle - currentAngle);
        float arc = delta > 180? 360 - delta:delta;
        return arc;
    }

    
}
