using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerRaycast : MonoBehaviour
{
    public float range = 100f; // Max distance the raycast will check for objects
    public TextMeshProUGUI promptText;
    public TextMeshProUGUI successText;
    public TextMeshProUGUI playbackText;
    public Image progressBarFill; // Assign this in the inspector
    private bool isLookingAtInteractable = false;
    private float holdTime = 5f; // Time in seconds to hold 'E'
    private float holdCounter = 0f; // Counter for how long 'E' has been held
    private float delayCounter = 0f;
    private float delayTotal = 3f;
    public List<AudioClip> recordedSounds = new List<AudioClip>();
    public AudioSource audioSource;

    public AudioClip finishedRecordingSound;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectObjects();

        if (Input.GetKeyDown(KeyCode.P) && !audioSource.isPlaying)
        {
            PlayLastRecordedSound();
        }

        
    }

    void ResetProgressBar()
    {
        holdCounter = 0f;
        progressBarFill.fillAmount = 0;
        
    }

    void PlayLastRecordedSound()
    {
        if (recordedSounds.Count > 0)
        {
            AudioClip lastRecordedClip = recordedSounds[recordedSounds.Count - 1];
            audioSource.PlayOneShot(lastRecordedClip);
            Debug.Log("Playing back last recorded sound: " + lastRecordedClip.name);
        }
        else
        {
            Debug.Log("No sounds have been recorded.");
        }
    }

    void DetectObjects()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.transform.CompareTag("Interactable")) // Make sure your object has this tag
            {
                promptText.gameObject.SetActive(true); // Show the prompt
                isLookingAtInteractable = true;
            }
            else
            {
                promptText.gameObject.SetActive(false); // Hide the prompt if not looking at interactable object
                isLookingAtInteractable = false;
                successText.gameObject.SetActive(false);
                playbackText.gameObject.SetActive(false);
            }
        }
        else
        {
            promptText.gameObject.SetActive(false); // Hide the prompt if raycast hits nothing
            isLookingAtInteractable = false;
        }

        //Hold down E to record a sound
        if (isLookingAtInteractable && Input.GetKey(KeyCode.E) && !recordedSounds.Contains(audioSource.clip))
        {
            // Increment the counter based on time
            progressBarFill.transform.parent.gameObject.SetActive(true); // Make sure the progress bar is visible
            holdCounter += Time.deltaTime;
            progressBarFill.fillAmount = holdCounter / holdTime; // Update progress bar fill
            promptText.gameObject.SetActive(false);
            successText.gameObject.SetActive(false);
            playbackText.gameObject.SetActive(false);

            //if the counter reaches 5 seconds record the sound
            if (holdCounter >= holdTime)
            {
                Debug.Log("Action completed!");

                // Reset for next use
                progressBarFill.transform.parent.gameObject.SetActive(false);
                successText.gameObject.SetActive(true);
                playbackText.gameObject.SetActive(true);
                //delayCounter += Time.deltaTime;
                

                // Check if the object we're looking at is the sound-emitting object
                AudioSource audioSource = hit.collider.GetComponent<AudioSource>();
                if (audioSource != null && !recordedSounds.Contains(audioSource.clip))

                {
                    // "Record" the sound by accessing the audio clip
                    AudioClip recordedClip = audioSource.clip;
                    recordedSounds.Add(recordedClip);
                    Debug.Log("Sound recorded from: " + hit.collider.name + " | Clip: " + recordedClip.name);
                    
                    PlayFinishedRecordingSound();

                }
            }


        }
        else if (progressBarFill.transform.parent.gameObject.activeSelf)
        {
            // If 'E' is released before completion, reset everything
            ResetProgressBar();
        }

        void PlayFinishedRecordingSound()
        {
            if (finishedRecordingSound != null)
            {
                audioSource.PlayOneShot(finishedRecordingSound);
                
            }
        }
    }
}

