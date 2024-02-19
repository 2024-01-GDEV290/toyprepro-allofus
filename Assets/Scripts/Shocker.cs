using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shocker : MonoBehaviour
{
	float shocker_intensity = 0;
	float charge = 0;
	
	[SerializeField]
	private Light intensity_light;
	
	[SerializeField]
	private AudioSource intensity_audio;
	
	[SerializeField]
	private AudioSource charge_audio;
	
	[SerializeField]
	private AudioSource ding;
	
	[SerializeField]
	private GameObject indicator;
	
	bool is_ready = false;
	
    // Start is called before the first frame update
    void Start()
    {
        indicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		shocker_intensity = Input.GetAxis("Charge Shocker");
		
		intensity_light.intensity = shocker_intensity;
		intensity_audio.volume = shocker_intensity;
		intensity_audio.pitch = shocker_intensity/2 + 1;
		
		power_up(shocker_intensity, charge_audio);
		Debug.Log(charge + " : " + is_ready);
    }
	
	void power_up(float intensity, AudioSource noise)
	{
		if (intensity >= 0.3 && intensity <= 0.6 && is_ready == false)
		{
			charge += 1;
			noise.Play();
		} else {
			noise.Stop();
		}
		
		if (charge == 3000)
		{
			is_ready = true;
			ding.Play();
		}
	}
	
	void shoot()
	{
		
	}
}
