using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDependentLight : MonoBehaviour
{
    [SerializeField][Range(0, 360)] float brightestTime;
    private Light light;
    private void Awake()
    {
        light = GetComponent<Light>();
    }
    void SetIntensity()
    {
        light.intensity = Mathf.Abs(brightestTime - 0);
    }
}
