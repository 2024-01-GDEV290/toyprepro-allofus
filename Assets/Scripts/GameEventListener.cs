using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEventTrigger trigger;
    public UnityEvent response;

    public void Respond()
    {
        response.Invoke();
    }

    private void OnEnable()
    {
        trigger.Register(this);
    }

    private void OnDisable()
    {
        trigger.Unregister(this);
    }
}
