using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryParticles : MonoBehaviour
{
    public ParticleSystem zap;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        zap.gameObject.transform.parent = null;
        zap.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
