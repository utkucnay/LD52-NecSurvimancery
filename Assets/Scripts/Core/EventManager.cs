using System;
using System.Collections.Generic;
using UnityEngine;


//thanks to Bernardo Pacheco

public class EventManager : Singleton<EventManager>
{
    private Dictionary<string, Action<Dictionary<string, object>>> eventDictionary;

    public override void Awake()
    {
        base.Awake();
        eventDictionary = new Dictionary<string, Action<Dictionary<string, object>>>();
    }

    public void StartListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        Action<Dictionary<string, object>> thisEvent;

        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StopListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        if (s_Instance == null) return;
        Action<Dictionary<string, object>> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            eventDictionary[eventName] = thisEvent;
        }
    }

    public void TriggerEvent(string eventName, Dictionary<string, object> message)
    {
        Action<Dictionary<string, object>> thisEvent = null;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(message);
        }
    }
}
