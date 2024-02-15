using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ETimeState{
    Idle,
    Winding,
    ActorMovement
}
public class Crank : MonoBehaviour
{
    [Header("===Set in Inspector===")]
    [SerializeField] GameObject cylinder;
    [SerializeField] Light dayLight;
    private float dayIntensity;
    [SerializeField] Light nightLight;
    private float nightIntensity;
    [SerializeField] float timeFlowRate = 10;
    [SerializeField] TextMeshProUGUI currentAngleDisplay;
    [SerializeField] TextMeshProUGUI currentTimeDisplay;

    [Header("NPC")]
    [SerializeField] GameObject npc;


    [Header("===Set Dynamically===")]
    [SerializeField] float cylinderRotation;
    [Header("NPC")]
    [SerializeField] Vector3 charStartPos;
    [SerializeField] Vector3 charEndPos;
    [SerializeField] ETimeState timeState; 

    private void Awake()
    {
        timeState = ETimeState.Idle;
        cylinderRotation = cylinder.transform.localEulerAngles.y;
        dayIntensity = dayLight.intensity;
        nightIntensity = nightLight.intensity;
        charStartPos = npc.transform.position + new Vector3(2,0,2);
        charEndPos = npc.transform.position + new Vector3(-2,0,-2);
        npc.transform.position = charStartPos; 
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            RotateClockwise();
        } else
        {
            if(timeState != ETimeState.Idle) timeState = ETimeState.Idle;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            RotateCounterClockwise();
        }
        else
        {
            if (timeState != ETimeState.Idle) timeState = ETimeState.Idle;
        }
        cylinderRotation = cylinder.transform.localEulerAngles.y;
        if (cylinderRotation < 0)
        {
            cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, 360 + cylinderRotation, cylinder.transform.localEulerAngles.z);
        }

        // The 360 degrees of the cylinder's rotation = 24 hours
        // 360/24 = 15 degrees per hour
        // 1 min = 15 degrees/60 minutes = .25 degrees


        dayLight.intensity = CalculateArc(cylinderRotation,180)/180 * dayIntensity;
        nightLight.intensity = CalculateArc(cylinderRotation, 0)/180 * nightIntensity;
        npc.transform.position = Vector3.Lerp(charStartPos, charEndPos, CalculateArc(cylinderRotation, 180)/180);
        UpdateUI();
    }

    string GetCurrentTimeString()
    {
        float currentHour = Mathf.Floor(cylinderRotation / 15);
        float currentMinute = Mathf.Floor((cylinderRotation - currentHour * 15) / .25f);
        return $"{currentHour}:{currentMinute}";
    }

    void UpdateUI()
    {
        currentAngleDisplay.text = $"Current Rotation: {cylinderRotation}";
        currentTimeDisplay.text = $"Current Time: {GetCurrentTimeString()}";
    }


    void RotateClockwise()
    {
        if (timeState != ETimeState.Winding) timeState = ETimeState.Winding;
        cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, cylinderRotation + timeFlowRate * Time.deltaTime,cylinder.transform.localEulerAngles.z);
    }

    void RotateCounterClockwise()
    {
        if (timeState != ETimeState.Winding) timeState = ETimeState.Winding;
        cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, cylinderRotation - timeFlowRate * Time.deltaTime, cylinder.transform.localEulerAngles.z);
    }
    float CalculateArc(float currentAngle,float brightestAngle)
    {
        float delta = Mathf.Abs(brightestAngle - currentAngle);
        float arc = delta > 180? 360 - delta:delta;
        return arc;
    }

    
}
