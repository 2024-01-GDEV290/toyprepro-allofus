using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
