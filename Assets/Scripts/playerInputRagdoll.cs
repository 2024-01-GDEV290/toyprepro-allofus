using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInputRagdoll : MonoBehaviour
{
	// float values for joystick and trigger input from Xbox controller
    float ls_vertical = 0;
    float ls_horizontal = 0;
	
	float rs_vertical = 0;
    float rs_horizontal = 0;

	float left_trigger = 0;
	float right_trigger = 0;
	
	// Both joystick input axis thresholds for the vertical and horizontal axis
    // are between -1 and 0.
    double lowerThreshold = -1;
    double upperThreshold = -0.2;
	
	// values used for translating input into gameplay actions
	
	float lh_swimming_intensity = 0;
	float rh_swimming_intensity = 0;
	
	// GameObject references for arm movement
	public GameObject rightShoulder;
	public GameObject rightElbow;
	
	public GameObject leftShoulder;
	public GameObject leftElbow;

	public GameObject leftKnee;
	public GameObject rightKnee;
	
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Detection for left-hand swim (using thresholds from global vars above)
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

        // Detection for triggers, which are used for left and right feet swimming
        left_trigger = Input.GetAxis("left_trigger");
		right_trigger = Input.GetAxis("right_trigger");

		if (left_trigger != 0)
		{
			moveLeftKnee(leftKnee, left_trigger);
		}
		if (right_trigger != 0)
		{
			moveRightKnee(rightKnee, right_trigger);
		}
		
		
		// Test animations for arms
		moveRightArm(rightShoulder, rightElbow, rh_swimming_intensity);
		moveLeftArm(leftShoulder, leftElbow, lh_swimming_intensity);
    }
	
	void moveRightArm(GameObject shoulder, GameObject elbow, float intensity)
	{
		float shoulder_degrees = 0;
		float elbow_degrees = 0;
		
		if (intensity != 0)
		{
			shoulder_degrees = (-intensity * 60);
			elbow_degrees = (-intensity * 60);
		} else
		{
			
		}
		
		shoulder.transform.localRotation = Quaternion.Euler(0, 0, shoulder_degrees);
		elbow.transform.localRotation = Quaternion.Euler(0, 0, elbow_degrees);
	}
	
	void moveLeftArm(GameObject shoulder, GameObject elbow, float intensity)
	{
		float shoulder_degrees = 0;
		float elbow_degrees = 0;
		
		if (intensity != 0)
		{
			shoulder_degrees = (intensity * 60);
			elbow_degrees = (intensity * 60);
		} else
		{
			
		}
		
		shoulder.transform.localRotation = Quaternion.Euler(0, 0, shoulder_degrees);
        elbow.transform.localRotation = Quaternion.Euler(0, 0, elbow_degrees);
	}

	void moveLeftKnee(GameObject knee, float intensity)
	{
		float knee_degrees = 0;

		if (intensity != 0)
		{
			knee_degrees = (-intensity * 60);
		} else
		{

		}

		knee.transform.localRotation = Quaternion.Euler(0, knee_degrees, 0);
	}

    void moveRightKnee(GameObject knee, float intensity)
    {
        float knee_degrees = 0;

        if (intensity != 0)
        {
            knee_degrees = (-intensity * 60);
        }
        else
        {

        }

        knee.transform.localRotation = Quaternion.Euler(0, knee_degrees, 0);
    }
}