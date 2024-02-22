using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public Rigidbody rb;
  public int speed;

  void FixedUpdate()
  {
	  if(Input.GetKey("w"))
	  {
		  rb.AddForce(0,0, speed); // x,y,z
	  }
	  if(Input.GetKey("a"))
	  {
		  rb.AddForce(-speed, 0,0);
	  }
	  if(Input.GetKey("d"))
	  {
		  rb.AddForce(speed ,0,0);
	  }
	  if(Input.GetKey("s"))
	  {
		  rb.AddForce(0,0, -speed);
	  }
	  if(Input.GetKey("space"))
	  {
		  rb.AddForce(0,speed,0);
	  }
  }



}
