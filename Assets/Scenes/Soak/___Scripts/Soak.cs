using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soak : MonoBehaviour
{

    public Animator animator;
    public ParticleSystem splash;

    bool isWet = false;
    int dryTimer = 200;

    // Update is called once per frame
    void Update()
    {
        if (isWet == true)
        {
            dryTimer -= 1;
        }

        if (dryTimer <= 15) // Play animation before timer resets
        {
            animator.SetBool("Soaked", false);
        }

        if (dryTimer <= 0)
        {
            isWet = false;
            dryTimer = 200;
        }
    }

    private void OnParticleCollision(GameObject other)
    {   
        if (isWet == false)
        {
            animator.SetBool("Soaked", true);
            splash.Play();
            isWet = true;
        }
    }
}
