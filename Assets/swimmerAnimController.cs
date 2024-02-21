using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swimmerAnimController : MonoBehaviour
{

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim != null)
        {
            if (Input.GetKeyDown("q"))
            {
               anim.SetTrigger("leftArm");
            }

            if (Input.GetKeyUp("q"))
            {
                anim.SetTrigger("leftArm");
            }

            if (Input.GetKeyDown("w"))
            {
                anim.SetTrigger("leftLeg");
            }

            if (Input.GetKeyDown("o"))
            {
                anim.SetTrigger("rightArm");
            }

            if (Input.GetKeyDown("p"))
            {
                anim.SetTrigger("rightLeg");
            }
        }
        
    }
}
