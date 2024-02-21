using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarTarget : MonoBehaviour
{
    [SerializeField] Material hiddenMaterial;
    [SerializeField] Material visibleMaterial;

    public void RevealSelf(float revealTime)
    {
        GetComponent<MeshRenderer>().material = visibleMaterial;
        Invoke(nameof(HideSelf), revealTime);
    }

    void HideSelf()
    {
        GetComponent<MeshRenderer>().material = hiddenMaterial;
    }
}
