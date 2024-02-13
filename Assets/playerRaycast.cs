using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerRaycast : MonoBehaviour
{
    public float range = 100f; // Max distance the raycast will check for objects
    public TextMeshProUGUI promptText;
    public Image progressBarFill; // Assign this in the inspector
    private bool isLookingAtInteractable = false;
    private float holdTime = 5f; // Time in seconds to hold 'E'
    private float holdCounter = 0f; // Counter for how long 'E' has been held



    // Update is called once per frame
    void Update()
    {
        DetectObjects();

        if (isLookingAtInteractable && Input.GetKey(KeyCode.E))
        {
            // Increment the counter based on time
            holdCounter += Time.deltaTime;
            progressBarFill.fillAmount = holdCounter / holdTime; // Update progress bar fill
            progressBarFill.transform.parent.gameObject.SetActive(true); // Make sure the progress bar is visible
            promptText.gameObject.SetActive(false);

            if (holdCounter >= holdTime)
            {
                Debug.Log("Action completed!");
                // Reset for next use
                ResetProgressBar();
            }
        }
        else if (progressBarFill.transform.parent.gameObject.activeSelf)
        {
            // If 'E' is released before completion, reset everything
            ResetProgressBar();
        }
    }

    void ResetProgressBar()
    {
        holdCounter = 0f;
        progressBarFill.fillAmount = 0;
        progressBarFill.transform.parent.gameObject.SetActive(false);
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
            }
        }
        else
        {
            promptText.gameObject.SetActive(false); // Hide the prompt if raycast hits nothing
            isLookingAtInteractable = false;
        }
    }
}
