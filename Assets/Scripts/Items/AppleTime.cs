using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppleTime : MonoBehaviour
{
    private Animator anim;

    [Header("Set dynamically")]
    public bool animating;
    public bool onTree;
    public float ripeness;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        ripeness = 0;
        animating = false;
        onTree = false;
    }

    public void AppleAnim()
    {
        if (!animating) { return; }

        ripeness = (WindingTime.S.hours - 2f) / 20f;

        if (ripeness >= 1) { Destroy(this.gameObject); }
        else
        {
            anim.Play("AppleAnim", 0, ripeness);
            if (ripeness >= 0.4) { onTree = false; }
        }
    }
}
