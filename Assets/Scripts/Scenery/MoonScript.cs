using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void MoveMoon()
    {
        float frame = WindingTime.S.hours / 24f;
        anim.Play("MoonAnim", 0, frame);
    }
}
