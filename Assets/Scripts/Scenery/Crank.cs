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

public enum ERotate
{
    idle,
    forward,
    reverse
}

public class Crank : MonoBehaviour
{
    [Header("===Set in Inspector===")]
    [SerializeField] GameObject cylinder;
    [SerializeField] float timeFlowRate = 10;
    [SerializeField] TextMeshProUGUI currentAngleDisplay;
    [SerializeField] TextMeshProUGUI currentTimeDisplay;
    public float actorMoveSpeed = 10;
    //[SerializeField] Gradient skyGradient;
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
    public ETimeState timeState;
    public ERotate rotate;

    private void Awake()
    {
        timeState = ETimeState.Idle;
        rotate = ERotate.idle;
        timeAsRotation = cylinder.transform.localEulerAngles.y;
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        MoveActors();
    }
    private void Update()
    {
        
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
        else if (timeAsRotation > 360)
        {
            cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, timeAsRotation - 360, cylinder.transform.localEulerAngles.z);
        }

        // The 360 degrees of the cylinder's rotation = 24 hours
        // 360/24 = 15 degrees per hour
        // 1 min = 15 degrees/60 minutes = .25 degrees

        // Update UI and environment visuals
        //UpdateUI();
        SkyChange();
    }

    public void StartTicking()
    {
        timeState = ETimeState.ActorMovement;
        audioSource.clip = tickSound;
        audioSource.Play();
        MoveActors();
        Invoke("StopTicking", 2);

        if (rotate == ERotate.forward)
        {
            if (timeAsRotation > WindingTime.S.degrees) { WindingTime.S.AdvanceTime((int) timeAsRotation - WindingTime.S.degrees); }
            else { WindingTime.S.AdvanceTime((360 + (int) timeAsRotation) - WindingTime.S.degrees); }
        }
        else if (rotate == ERotate.reverse)
        {
            if (timeAsRotation < WindingTime.S.degrees) { WindingTime.S.RewindTime(WindingTime.S.degrees - (int) timeAsRotation); }
            else { WindingTime.S.RewindTime((360 + WindingTime.S.degrees) - (int)timeAsRotation); }
        }
        rotate = ERotate.idle;
        cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, WindingTime.S.degrees, cylinder.transform.localEulerAngles.z);
        Debug.Log("Degrees: " + WindingTime.S.degrees.ToString());
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

    void SkyChange()
    {
        celestialBodiesTransform.eulerAngles = new Vector3(0, 0, -timeAsRotation);
        //Camera.main.backgroundColor = skyGradient.Evaluate(CalculateArc(timeAsRotation, 180) / 180)
;    }

    //string GetCurrentTimeString()
    //{
    //    float currentHour = Mathf.Floor(timeAsRotation / 15);
    //    float currentMinute = Mathf.Floor((timeAsRotation - currentHour * 15) / .25f);
    //    return $"{currentHour}:{currentMinute}";
    //}

    //void UpdateUI()
    //{
    //    currentAngleDisplay.text = $"Current Rotation: {timeAsRotation}";
    //    currentTimeDisplay.text = $"Current Time: {GetCurrentTimeString()}";
    //}

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
        float newRotation = timeAsRotation + rotationAmount;
        cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, newRotation, cylinder.transform.localEulerAngles.z);
        rotate = ERotate.forward;
    }

    void RotateCounterClockwise()
    {
        if (!audioSource.isPlaying)
        {
            PlayClick();
        }
        float rotationAmount = timeFlowRate * Time.deltaTime;
        float newRotation = timeAsRotation - rotationAmount;
        cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, newRotation, cylinder.transform.localEulerAngles.z);
        rotate = ERotate.reverse;
    }

    float CalculateArc(float currentAngle,float targetAngle)
    {
        float delta = Mathf.Abs(targetAngle - currentAngle);
        float arc = delta > 180? 360 - delta:delta;
        return arc;
    }

    
}
