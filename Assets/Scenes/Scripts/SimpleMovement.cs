
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public State state;
    //public float chargeValue;
    //public float currentCharge;
    private Rigidbody rb;
    private bool onGround = true;

    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        var main = ps.main;
        state.Charge = 0;
        state.chargeValue = 250;
        state.allPowered = 0;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime *3);
            if(state.Charge <1000 && onGround)
            {
                state.Charge += (state.chargeValue *Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(Vector3.back * Time.deltaTime*3);
            if (state.Charge < 1000 && onGround)
            {
                state.Charge += (state.chargeValue * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(Vector3.up, -1);
            if (state.Charge < 1000 && onGround)
            {
                state.Charge += ((state.chargeValue /2) * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(Vector3.up, 1);
            if (state.Charge < 1000 && onGround)
            {
                state.Charge += ((state.chargeValue / 2) * Time.deltaTime);
            }
        }
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            onGround = false;
        }
        else if (state.Charge > 0)
        {
            state.Charge -= 150*Time.deltaTime;
        }
        if (state.Charge > 250)
        {
            //particales
            //main. (stuff) = rate?
        }
        else if(state.Charge >500)
        {
            //more particales
            //main.stuff = more rate?
        }
        else if(state.Charge> 750)
        {
            //ALL THE PARTICALES
            //main.stuff = all the rate
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerCell"))
        {
            if(state.Charge >= 500 && other.GetComponent<PowerCells>().sentCharge != 1000)
            {
                //other.gameObject.SetActive(false);
                //other.GetComponent<Renderer>().material.color = new Color(100, 100, 100);
                other.GetComponent<PowerCells>().sentCharge = 1000;
                state.Charge -= 500;
                state.allPowered += 1;
                //state.sentCharge1 = 1000;
                
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Floor"))
        {
            onGround = true;
        }
    }
}  