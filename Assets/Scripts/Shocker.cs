using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shocker : MonoBehaviour
{
	//shocker_intensity is the raw 0 to 1 float input from the trigger.
	float shocker_intensity;
	
	//The following three vars are used for identification of the shocker's power level.
	[SerializeField]
	private Light intensity_light;
	
	[SerializeField]
	private AudioSource intensity_audio;

	RaycastHit sro_data;
	RaycastHit src_data;
	
	[SerializeField]
	private GameObject ray_end;
	
	
	private bool is_shocking = false;
	private bool closed_interact = false;
	
	public opengate opengate;
	public egate_closed egate;
	
	public GameObject lightning;
	
    // Start is called before the first frame update
    void Start()
    {
		ray_end.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		shocker_intensity = Input.GetAxis("Charge Shocker");
		
		//Used for idenfication of power level on shocker model
		intensity_light.intensity = shocker_intensity;
		intensity_audio.volume = shocker_intensity;
		intensity_audio.pitch = shocker_intensity/2 + 0.5F;
		
		
		Physics.Raycast(transform.position, transform.forward, out sro_data, 10);
		Physics.Raycast(transform.position, transform.forward, out src_data, 10);
		
		lightning.transform.Rotate(0, 0, 10, Space.Self);
		
		if (shocker_intensity != 0)
		{
			shock_connect(sro_data);
			lightning.SetActive(true);


		} else {
			is_shocking = false;
			lightning.SetActive(false);
		}
		
		
		if(sro_data.collider.tag == "closed")
		{
			closed_interact = true;
		} else {
			closed_interact = false;
		}
		
    }
	
	void shock_connect(RaycastHit data)
	{
		
		
		if (data.collider.tag == "open")
		{
			is_shocking = true;
		}
		
		opengate.updateLightning(ray_end.transform.position);
		
		
		if (is_shocking == true && closed_interact == true)
		{
			egate.open_door();
		}
	}
}
