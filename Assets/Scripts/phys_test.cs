using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phys_test : MonoBehaviour
{
	public Rigidbody rb;
	
	[SerializeField]
	float speed_multiplier = 40;
	[SerializeField]
	float velocity_cap = 20;
	
	// For Xbox controller input
	float ls_vertical = 0;
    float ls_horizontal = 0;
	
	float rs_vertical = 0;
    float rs_horizontal = 0;
	
	float lh_swimming_intensity = 0;
	float rh_swimming_intensity = 0;
	
	double lowerThreshold = -1;
    double upperThreshold = -0.2;

	public float thrust = 1f;
	
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		ls_vertical = Input.GetAxis("Vertical");
        ls_horizontal = Input.GetAxis("Horizontal");
		
		lh_swimming_intensity = 0;

        if (ls_vertical >= lowerThreshold && ls_vertical <= upperThreshold)
        {
            if (ls_horizontal >= lowerThreshold && ls_horizontal <= upperThreshold)
            {
				lh_swimming_intensity = ls_vertical * ls_horizontal;
            }
		}
		
		pushLeft(lh_swimming_intensity);
		
		// Detection for right-hand swim, using similar method from above
        rs_vertical = Input.GetAxis("rs_vertical") * -1;
        rs_horizontal = Input.GetAxis("rs_horizontal") * -1;
		
		rh_swimming_intensity = 0;
		
		if (rs_vertical >= lowerThreshold && rs_vertical <= upperThreshold)
        {
            if (rs_horizontal >= lowerThreshold && rs_horizontal <= upperThreshold)
            {
				rh_swimming_intensity = rs_vertical * rs_horizontal;
            }
		}
		
		pushRight(rh_swimming_intensity);
		
		
		
		
		/*
        float left_trigger_input = Input.GetAxis("left_trigger");
		pushLeft(left_trigger_input);
		
		float right_trigger_input = Input.GetAxis("right_trigger");
		pushRight(right_trigger_input);
		*/
		
		Debug.Log(rb.angularVelocity);
	}
	
	void pushLeft(float input)
	{
		input *= speed_multiplier;
		if (rb.velocity.z > velocity_cap)
		{
		
		} else {
			rb.AddRelativeForce((input * Time.deltaTime), (input * Time.deltaTime), (-input * Time.deltaTime)/6, ForceMode.Impulse);
			rb.AddTorque(0, (input * Time.deltaTime), -(input/2 * Time.deltaTime));
			//rb.AddForce(transform.forward * thrust);
		}
	}
	
	void pushRight(float input)
	{
		input *= speed_multiplier;
		if (rb.velocity.z > velocity_cap)
		{
		
		} else {
            rb.AddRelativeForce(-(input * Time.deltaTime), (input * Time.deltaTime), (-input * Time.deltaTime)/6, ForceMode.Impulse);
            rb.AddTorque(0, -(input * Time.deltaTime), -(input/2 * Time.deltaTime));
            //rb.AddForce(transform.forward * thrust);
        }
	}
}
