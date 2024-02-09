using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Crank : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] GameObject cylinder;
    [SerializeField] Light dayLight;
    [SerializeField] Light nightLight;

    [Header("Set Dynamically")]
    [SerializeField] float cylinderRotation;

    private void Awake()
    {
        cylinderRotation = cylinder.transform.localEulerAngles.y;
    }
    private void Update()
    {
        cylinderRotation = cylinder.transform.localEulerAngles.y;
        if (cylinderRotation < 0)
        {
            cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, 360 + cylinderRotation, cylinder.transform.localEulerAngles.z);
        }

        
        dayLight.intensity = CalculateArc(cylinderRotation,180) * 10;
        nightLight.intensity = CalculateArc(cylinderRotation, 0) * 10;
    }

    float CalculateArc(float currentAngle,float brightestAngle)
    {
        float arc;
        float delta = Mathf.Abs(brightestAngle - currentAngle);
        arc = delta > 180? 360 - delta:delta;
        return arc;
    }

    
}
