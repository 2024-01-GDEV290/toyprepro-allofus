using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyColor : MonoBehaviour
{
    [SerializeField] Gradient skyGradient;

    private void Awake()
    {
        Camera.main.backgroundColor = skyGradient.Evaluate(0);
    }

    public void SkyChange()
    {
        Camera.main.backgroundColor = skyGradient.Evaluate(Mathf.Abs(WindingTime.S.hours - 12f) / 12f);
    }
}
