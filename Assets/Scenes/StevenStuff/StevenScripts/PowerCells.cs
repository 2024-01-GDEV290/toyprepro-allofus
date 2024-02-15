using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCells : MonoBehaviour
{
    public float sentCharge;

    // Start is called before the first frame update
    void Start()
    {
        sentCharge = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (sentCharge > 0)
        {
            this.GetComponent<Renderer>().material.color = new Color(0, 150, 255);
            sentCharge -= 150 * Time.deltaTime;
        }
        else
        {
            this.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
        }
    }
}
