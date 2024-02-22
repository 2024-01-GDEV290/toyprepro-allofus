using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shocker : MonoBehaviour
{
	//shocker_intensity is the raw 0 to 1 float input from the trigger.
	//shocker_power is that number, translated into int values between 0 and 3.
	float shocker_intensity;
	int shocker_power;
	
	//The following three vars are used for identification of the shocker's power level.
	[SerializeField]
	private Light intensity_light;
	
	[SerializeField]
	private AudioSource intensity_audio;

	[SerializeField]
	private TMP_Text intensity_meter;
	
	//Raycast used for shocking
	Ray shocker_ray;
	RaycastHit shocker_data;
	
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
		shocker_intensity = Input.GetAxis("Charge Shocker");
		
		//Used for idenfication of power level on shocker model
		intensity_light.intensity = shocker_intensity;
		intensity_audio.volume = shocker_intensity;
		intensity_audio.pitch = shocker_intensity/2 + 0.5F;

		//calculates shocker power level, also shows on intensity meter
		shocker_power = calculate_power(shocker_intensity);
		intensity_meter.text = (shocker_power.ToString());
		
		if (Input.GetButtonDown("Shoot Shocker"))
		{
			shock(shocker_power);
		}
    }
	
	int calculate_power(float intensity)
	{
		
		return (int)(shocker_intensity * 3);
	}
	
	void shock(int power)
	{
		shocker_ray = new Ray(transform.position, transform.forward);
		Physics.Raycast(shocker_ray, out shocker_data);
		
		switch(shocker_data.collider.tag)
		{
			case "level1":
				if (power == 1)
				{
					Debug.Log("Shocked circuit!!");
				} else {
					Debug.Log("Missed...");
				}
				break;
			case "level2":
				if (power == 2)
				{
					Debug.Log("Shocked circuit!!");
				} else {
					Debug.Log("Missed...");
				}
				break;
			case "level3":
				if (power == 3)
				{
					Debug.Log("Shocked circuit!!");
				} else {
					Debug.Log("Missed...");
				}
				break;
			case null:
				Debug.Log("Didn't hit anything...");
				break;
			default:
				Debug.Log("Not a circuit...");
				break;
		}
				
		
	}
}
