using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] GameObject sweep;
    [SerializeField] float rotationSpeed = 1;
    AudioSource playerAudioSource;
    [SerializeField] AudioClip sonarSound;

    private void Awake()
    {
         playerAudioSource = transform.parent.gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!playerAudioSource.isPlaying)
        {
            playerAudioSource.PlayOneShot(sonarSound);
        } 
        transform.eulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime, 0);
    }
}
