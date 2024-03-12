using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Actor : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] ScheduleEvent[] schedule;
    [SerializeField] Crank crank;
    public string displayName;

    [TextArea]
    [SerializeField] protected string defaultDialogue;
    [SerializeField] Item desiredItem;
    [TextArea][SerializeField] string satisfiedDialogue;
    [SerializeField] GameEventTrigger openGateTrigger;

    [Header("Set Dynamically")]
    [SerializeField] List<Item> inventory;
    [SerializeField] protected PlayerMotor player;
    [SerializeField] ScheduleEvent currentScheduleEvent;
    [SerializeField] GameObject wayPoint;

    private void Awake()
    {
        if (displayName == null) displayName = gameObject.name;

    }
    private void Update()
    {
        if (wayPoint != null && wayPoint.transform.position != transform.position) 
        {
            // We should convert this movement system to use Navmesh eventually. 
            Vector3 newPosition = new Vector3(wayPoint.transform.position.x, transform.position.y, wayPoint.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * crank.actorMoveSpeed);
        }
    }

    public void ReciteLines()
    {
        foreach (Item item in player.inventory)
        {
            if (item == desiredItem)
            {
                Debug.Log(satisfiedDialogue);
                TakeItem(item);
                openGateTrigger.Raise();
                return;
            }
        }
        Debug.Log(defaultDialogue);
    }

    void TakeItem(Item item)
    {
        if (player.inventory.Contains(item))
        {
            player.inventory.Remove(item);
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
            if (WindingTime.S.degrees >= e.startTime && WindingTime.S.degrees < e.endTime) return e;
        }
        return null; 
    }
}
