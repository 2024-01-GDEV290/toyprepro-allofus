using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
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
    [SerializeField] Light dayLight;
    private float dayIntensity;
    [SerializeField] Light nightLight;
    private float nightIntensity;
    [SerializeField] float timeFlowRate = 10;
    [SerializeField] TextMeshProUGUI currentAngleDisplay;
    [SerializeField] TextMeshProUGUI currentTimeDisplay;
    public float actorMoveSpeed = 10; 

/*    [Header("NPC")]
    [SerializeField] GameObject npc;*/

    [Header("Audio")]
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioSource clickSource;


    [Header("===Set Dynamically===")]
    public float timeAsRotation;
    [Header("NPC")]
    [SerializeField] Vector3 charStartPos;
    [SerializeField] Vector3 charEndPos;
    [SerializeField] ETimeState timeState;

    [SerializeField] float initialRotation;
    [SerializeField] float windupRotation;
    [SerializeField] float totalRotation;

    private void Awake()
    {
        windupRotation = 0;
        initialRotation = 0;
        timeState = ETimeState.Idle;
        timeAsRotation = cylinder.transform.localEulerAngles.y;
        dayIntensity = dayLight.intensity;
        nightIntensity = nightLight.intensity;
/*        charStartPos = npc.transform.position + new Vector3(2,0,2);
        charEndPos = npc.transform.position + new Vector3(-2,0,-2);
        npc.transform.position = charStartPos; */
    }
    private void Start()
    {
        MoveActors();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            timeState = ETimeState.WindingForward;
            initialRotation = timeAsRotation;
        } else if (Input.GetKeyUp(KeyCode.X))
        {
            timeState = ETimeState.ActorMovement;
            totalRotation = Mathf.Abs(initialRotation - windupRotation);
            MoveActors();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            timeState = ETimeState.WindingReverse;
            initialRotation = timeAsRotation;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            timeState = ETimeState.ActorMovement;
            totalRotation = Mathf.Abs(initialRotation - windupRotation);
            MoveActors();
        }

        if (timeState == ETimeState.WindingForward)
        {
            RotateClockwise();
        }
        else if (timeState == ETimeState.WindingReverse)
        {
            RotateCounterClockwise();
        } else if (timeState == ETimeState.ActorMovement)
        {

        }

        timeAsRotation = cylinder.transform.localEulerAngles.y;
        if (timeAsRotation < 0)
        {
            cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, 360 + timeAsRotation, cylinder.transform.localEulerAngles.z);
        }

        // The 360 degrees of the cylinder's rotation = 24 hours
        // 360/24 = 15 degrees per hour
        // 1 min = 15 degrees/60 minutes = .25 degrees


        ChangeTime();
/*        npc.transform.position = Vector3.Lerp(charStartPos, charEndPos, CalculateArc(timeAsRotation, 180)/180);*/
        UpdateUI();
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
        dayLight.intensity = CalculateArc(timeAsRotation, 180) / 180 * dayIntensity;
        nightLight.intensity = CalculateArc(timeAsRotation, 0) / 180 * nightIntensity;
    }
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
        clickSource.PlayOneShot(clickSound);
    }

    void RotateClockwise()
    {
        if (!clickSource.isPlaying)
        {
            PlayClick();
        }
        float rotationAmount = timeFlowRate * Time.deltaTime;
        windupRotation += rotationAmount;
        cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, timeAsRotation + rotationAmount,cylinder.transform.localEulerAngles.z);
    }

    void RotateCounterClockwise()
    {
        if (!clickSource.isPlaying)
        {
            PlayClick();
        }
        cylinder.transform.localEulerAngles = new Vector3(cylinder.transform.localEulerAngles.x, timeAsRotation - timeFlowRate * Time.deltaTime, cylinder.transform.localEulerAngles.z);
    }
    float CalculateArc(float currentAngle,float brightestAngle)
    {
        float delta = Mathf.Abs(brightestAngle - currentAngle);
        float arc = delta > 180? 360 - delta:delta;
        return arc;
    }

    
}
