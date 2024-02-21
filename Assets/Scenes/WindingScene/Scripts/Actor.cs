using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Actor : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] ScheduleEvent[] schedule;
    [SerializeField] Crank crank;

    [Header("Set Dynamically")]
    [SerializeField] ScheduleEvent currentScheduleEvent;
    [SerializeField] GameObject wayPoint;

    private void Update()
    {
        if (wayPoint != null && wayPoint.transform.position != transform.position) 
        {
            transform.position = Vector3.Lerp(transform.position, wayPoint.transform.position, Time.deltaTime * crank.actorMoveSpeed);
        }
    }

    public void MoveToScheduledLocation()
    {
        currentScheduleEvent = GetCurrentScheduleEvent();
        if (currentScheduleEvent != null)
        {
            wayPoint = GetWayPoint(currentScheduleEvent.wayPointName);   
        }
    }

/*    void OrientSprite()
    {
        Vector3 diff = Camera.main.transform.position - transform.position;
        if (Vector3.Angle(diff, transform.position) > 90)
        {
            // Orienting the decal to the correct direction so the particle system is facing towards the side the player is on. 
            decal.transform.localEulerAngles = new Vector3(-90, 0, 0);
        }
        else
        {
            decal.transform.localEulerAngles = new Vector3(90, 0, 0);
        }
    }*/
    GameObject GetWayPoint(string wayPointName)
    {
        return GameObject.Find(wayPointName);
    }

    ScheduleEvent GetCurrentScheduleEvent()
    {
        foreach (ScheduleEvent e in schedule)
        {
            if (crank.timeAsRotation >= e.startTime && crank.timeAsRotation < e.endTime) return e;
        }
        return null; 
    }
}
