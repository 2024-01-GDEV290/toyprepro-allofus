using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celestial : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Rotation()
    {
        float cycle = WindingTime.S.hours / 24f;
        anim.Play("CelestialAnim", 0, cycle);
    }
}
