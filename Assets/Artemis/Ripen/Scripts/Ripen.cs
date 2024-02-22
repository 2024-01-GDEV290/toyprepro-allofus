using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ripen : MonoBehaviour
{
    public GameObject ripenBtn;
    public GameObject stopBtn;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void StartRipen()
    {
        anim.SetBool("Ripening", true);
        ripenBtn.SetActive(false);
        stopBtn.SetActive(true);
    }

    public void StopRipen()
    {
        anim.speed = 0;
        stopBtn.SetActive(false);
    }
}
