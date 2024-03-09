using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void MoveSun()
    {
        float frame = WindingTime.S.degrees / 360f;
        anim.Play("SunAnim", 0, frame);
    }
}
