using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soak : MonoBehaviour
{

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {   
        animator.SetBool("Soaked", true);
    }
}
