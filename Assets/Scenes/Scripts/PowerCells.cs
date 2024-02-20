using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCells : MonoBehaviour
{
    public float sentCharge;
    public State state;

    // Start is called before the first frame update
    void Start()
    {
        sentCharge = 0;
        this.GetComponent<Renderer>().material.color = new Color(0, 0, 255);

    }

    // Update is called once per frame
    void Update()
    {
        if (sentCharge > 0)
        {
            this.GetComponent<Renderer>().material.color = new Color(0, 100, 255);
            //sentCharge -= 150 * Time.deltaTime;
        }

        if (state.allPowered == 4)
        {
            this.GetComponent<Renderer>().material.color = new Color(0, 100, 255);
            //state.gear.transform.Rotate(Vector3.up, 1);
            //state.gear.
        }
    }
}
