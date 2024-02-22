using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum ETimeState{
    Idle,
    WindingForward,
    WindingReverse,
    ActorMovement
}
public class Crank : MonoBehaviour
{
    [Header("===Set in Inspector===")]
    [SerializeField] GameObject cylinder;
    [SerializeField] float timeFlowRate = 10;
    [SerializeField] TextMeshProUGUI currentAngleDisplay;
    [SerializeField] TextMeshProUGUI currentTimeDisplay;
    public float actorMoveSpeed = 10;
    [SerializeField] Gradient skyGradient;
    [SerializeField] Transform celestialBodiesTransform;

    

    [Header("Audio")]
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip tickSound;


    [Header("===Set Dynamically===")]
    public float timeAsRotation;
    [Header("NPC")]
    [SerializeField] Vector3 charStartPos;
    [SerializeField] Vector3 charEndPos;
    [SerializeField] ETimeState timeState;

    private void Awake()
    {
        timeState = ETimeState.Idle;
        timeAsRotation = cylinder.transform.localEulerAngles.y;
        audioSource = GetComponent<AudioSource>();  
    }
    private void Start()
    {
        MoveActors();
    }
    private void Update()
    {
        // Handle crank input and state changes
      
            if (timeState == ETimeState.Idle && Input.GetKeyDown(KeyCode.X))
            {
                timeState = ETimeState.WindingForward;
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                StartTicking();
            }
            else if (timeState == ETimeState.Idle && Input.GetKeyDown(KeyCode.Z))
            {
                timeState = ETimeState.WindingReverse;
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                StartTicking();
            }
       
        
        if (timeState == ETimeState.WindingForward)
        {
            RotateClockwise();
        }
        else if (timeState == ETimeState.WindingReverse)
        {
            RotateCounterClockwise();
        }

        // Set the rotation to a positive rotational value
        timeAsRotation = cylinder.transform.localEulerAngles.y;
        if (timeAsRotation < 0)
        {
            cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, 360 + timeAsRotation, cylinder.transform.localEulerAngles.z);
        }

        // The 360 degrees of the cylinder's rotation = 24 hours
        // 360/24 = 15 degrees per hour
        // 1 min = 15 degrees/60 minutes = .25 degrees

        // Update UI and environment visuals
        ChangeTime();
        UpdateUI();
    }

    public void StartTicking()
    {
        timeState = ETimeState.ActorMovement;
        audioSource.clip = tickSound;
        audioSource.Play();
        MoveActors();
        Invoke("StopTicking", 2);
    }

    public void StopTicking()
    {
        audioSource.Stop();
        audioSource.clip = null;
        timeState = ETimeState.Idle;
    }
    void MoveActors()
    {
        foreach (Actor actor in FindObjectsOfType<Actor>())
        {
            actor.MoveToScheduledLocation();
        }
    }

    void ChangeTime()
    {
        celestialBodiesTransform.eulerAngles = new Vector3(-timeAsRotation,0,0);
        Camera.main.backgroundColor = skyGradient.Evaluate(CalculateArc(timeAsRotation, 180) / 180)
;    }

    string GetCurrentTimeString()
    {
        float currentHour = Mathf.Floor(timeAsRotation / 15);
        float currentMinute = Mathf.Floor((timeAsRotation - currentHour * 15) / .25f);
        return $"{currentHour}:{currentMinute}";
    }

    void UpdateUI()
    {
        currentAngleDisplay.text = $"Current Rotation: {timeAsRotation}";
        currentTimeDisplay.text = $"Current Time: {GetCurrentTimeString()}";
    }

    void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }

    void RotateClockwise()
    {
        if (!audioSource.isPlaying)
        {
            PlayClick();
        }
        float rotationAmount = timeFlowRate * Time.deltaTime;
        cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, timeAsRotation + rotationAmount,cylinder.transform.localEulerAngles.z);
    }

    void RotateCounterClockwise()
    {
        if (!audioSource.isPlaying)
        {
            PlayClick();
        }
        cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, timeAsRotation - timeFlowRate * Time.deltaTime, cylinder.transform.localEulerAngles.z);
    }
    float CalculateArc(float currentAngle,float targetAngle)
    {
        float delta = Mathf.Abs(targetAngle - currentAngle);
        float arc = delta > 180? 360 - delta:delta;
        return arc;
    }

    
}
