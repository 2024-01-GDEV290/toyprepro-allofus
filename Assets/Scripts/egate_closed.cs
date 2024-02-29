using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egate_closed : MonoBehaviour
{
	// Door that this gate opens when powered.
	public GameObject door;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
	
	public void open_door()
	{
		door.SetActive(false);
	}
}
