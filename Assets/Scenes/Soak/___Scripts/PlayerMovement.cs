using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 6f;
    public ParticleSystem rain;
    public AudioSource audio;


    [SerializeField] private Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            rain.Play();
            audio.Play();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rain.Stop();
            audio.Stop();
        }
    }

    


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
}
