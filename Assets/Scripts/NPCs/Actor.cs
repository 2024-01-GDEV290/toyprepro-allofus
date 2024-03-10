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

    [TextArea]
    [SerializeField] string defaultDialogue;
    [SerializeField] List<Item> itemsNoticed;
    [SerializeField] List<string> itemReactions;
    Dictionary<Item, string> reactionTable;

    [Header("Set Dynamically")]
    [SerializeField] PlayerMotor player;
    [SerializeField] ScheduleEvent currentScheduleEvent;
    [SerializeField] GameObject wayPoint;

    private void Awake()
    {
        reactionTable = new Dictionary<Item, string>();
        player = GameObject.Find("Player").GetComponent<PlayerMotor>();
        if (itemsNoticed.Count > 0 )
        {
            for (int i = 0; i < itemsNoticed.Count; i += 1)
            {
                string reaction = "Generic item reaction dialogue!";
                if (itemReactions.Count >= i + 1)
                {
                    reaction = itemReactions[i];
                }

                reactionTable.Add(itemsNoticed[i], reaction);

            }
        }

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
            if (itemsNoticed.Contains(item))
            {
                Debug.Log(reactionTable[item]);
                TakeItem(item);
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
