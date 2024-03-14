using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawn : MonoBehaviour
{
    public GameObject applePrefab;

    public void MakeApple()
    {
        if (WindingTime.S.hours == 2)
        {
            GameObject newApple = Instantiate(applePrefab);
            newApple.transform.SetParent(this.transform);
            newApple.transform.localPosition = Vector3.zero;
            newApple.GetComponent<AppleTime>().animating = true;
            newApple.GetComponent<AppleTime>().onTree = true;
        }
    }
}
