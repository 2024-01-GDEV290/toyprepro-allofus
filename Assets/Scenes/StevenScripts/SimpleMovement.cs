
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public State state;
    public float chargeValue;
    public float currentCharge;
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime);
            if(state.Charge <1000)
            {
                state.Charge += chargeValue;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(Vector3.back * Time.deltaTime);
            if (state.Charge < 1000)
            {
                state.Charge += chargeValue;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(Vector3.up, -1);
            if (state.Charge < 1000)
            {
                state.Charge += (chargeValue/2);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(Vector3.up, 1);
            if (state.Charge < 1000)
            {
                state.Charge += (chargeValue/2);
            }
        }
        else if (state.Charge > 0)
        {
            state.Charge--;
        }
        currentCharge = state.Charge;
    }
}  