using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shocker : MonoBehaviour
{
	float shocker_intensity = 0;
	
	[SerializeField]
	private Light intensity_light;
	
	[SerializeField]
	private AudioSource intensity_audio;

	[SerializeField]
	private TMP_Text intensity_meter;

	[SerializeField]
	private GameObject bolt;
	
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
		shocker_intensity = Input.GetAxis("Charge Shocker");
		
		intensity_light.intensity = shocker_intensity;
		intensity_audio.volume = shocker_intensity;
		intensity_audio.pitch = shocker_intensity/2 + 0.5F;

		int intensity_setting = (int)(shocker_intensity * 3);
		intensity_meter.text = (intensity_setting.ToString());
		
		if (Input.GetButtonDown("Shoot Shocker"))
		{
			shoot();
		}


    }
	
	void shoot()
	{
		Instantiate(bolt, transform.position, Quaternion.identity);
	}
}
