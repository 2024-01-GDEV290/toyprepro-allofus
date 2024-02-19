using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// Movement values
	float move_up_down;
	float move_left_right;
	
	float look_up_down;
	float look_left_right;
	
	float sprint;
	
	[SerializeField]
	private float movement_speed = 4;
	[SerializeField]
	private float look_sensitivity = 0.2F;
	
	[SerializeField]
	private GameObject player_camera;
	
	[SerializeField]
	private GameObject player_tool;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		move_up_down = Input.GetAxis("Move Forward/Backward");
		move_left_right = Input.GetAxis("Move Left/Right");
		sprint = Input.GetAxis("Sprint") + 1;
		move(move_up_down, move_left_right, sprint);
		
		look_up_down = Input.GetAxis("Look Up/Down");
		look_left_right = Input.GetAxis("Look Left/Right");
		look(look_up_down, look_left_right, player_camera, player_tool);
    }
	
	void move(float up_down, float left_right, float sprint_add)
	{
		transform.position += transform.TransformDirection(new Vector3(0, 0, sprint_add * -1 * movement_speed * up_down * Time.deltaTime));
		transform.position += transform.TransformDirection(new Vector3(sprint_add * movement_speed * left_right * Time.deltaTime, 0, 0));
		
	}
	
	void look(float up_down, float left_right, GameObject camera, GameObject tool)
	{
		//Debug.Log(UnityEditor.TransformUtils.GetInspectorRotation(camera.transform).x);
	
		camera.transform.Rotate(up_down * look_sensitivity, 0, 0, Space.Self);
		tool.transform.position -= transform.TransformDirection(new Vector3(0, up_down * 0.25F * Time.deltaTime, 0));
		
		transform.Rotate(0, left_right * look_sensitivity, 0, Space.Self);
		
		//Included for item sway
		//tool.transform.position -= transform.TransformDirection(new Vector3(left_right * Time.deltaTime, 0, 0));
		
	}
}
