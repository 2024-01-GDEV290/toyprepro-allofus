using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opengate : MonoBehaviour
{
	
	private LineRenderer line_renderer;
	
    // Start is called before the first frame update
    void Start()
    {
        line_renderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void updateLightning(Vector3 new_position)
	{
		line_renderer.SetPosition(1, new_position);
	}

}
