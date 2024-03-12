using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleGate : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(false);
    }
}
