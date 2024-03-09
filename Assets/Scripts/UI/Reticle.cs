using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    [SerializeField] Color idleColor = Color.white;
    [SerializeField] Color validTargetColor = Color.green;

    public void IndicateIdle()
    {
        GetComponent<Image>().color = idleColor;
    }

    public void IndicateValidTarget()
    {
        GetComponent<Image>().color = validTargetColor;
    }
}
