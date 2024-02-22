using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelShift : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PanelOut()
    {
        anim.SetBool("PanelOut", true);
    }

    public void PanelIn()
    {
        anim.SetBool("PanelOut", false);
    }
}
