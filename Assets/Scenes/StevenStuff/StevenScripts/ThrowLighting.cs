using DigitalRuby.LightningBolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowLighting : MonoBehaviour
{
    [SerializeField] private GameObject bolt1;
    [SerializeField] private GameObject bolt2;
    [SerializeField] private GameObject bolt3;
    [SerializeField] private GameObject bolt4;
    private Boolean shocking = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //left click
        if (Input.GetKeyDown("mouse 0"))
        {
            bolt1.SetActive(true);
            bolt2.SetActive(true);
            bolt3.SetActive(true);
            bolt4.SetActive(true);
           
        }
        else if (Input.GetKeyUp("mouse 0"))
        {
            bolt1.SetActive(false);
            bolt2.SetActive(false);
            bolt3.SetActive(false);
            bolt4.SetActive(false);

        }
    }
}
