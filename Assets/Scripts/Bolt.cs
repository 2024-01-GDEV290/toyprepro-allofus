using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public int speed = 25;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move(speed);
    }

    void move(int speed)
    {
        transform.position += transform.TransformDirection(new Vector3(0, 0, speed * Time.deltaTime));
    }
}
