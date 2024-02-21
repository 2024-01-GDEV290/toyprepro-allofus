using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarSweep : MonoBehaviour
{
    [SerializeField] float revealTime = 1.0f;
    private void OnTriggerEnter(Collider other)
    {
        SonarTarget target = other.gameObject.GetComponent<SonarTarget>();
        if ( target != null)
        {
            target.RevealSelf(revealTime);
        }
    }
}
