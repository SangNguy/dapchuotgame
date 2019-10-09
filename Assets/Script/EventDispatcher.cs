using UnityEngine;
using System.Collections.Generic;
using System;

public enum EventID
{
    None = 0,
    HitTheMouse,
    HitOldman,
    HitTheDiamond,
    HitCombo,
    OverGame,
    StartGame,
    SelectMap,
}

public class EventDispatcher : MonoBehaviour
{
    #region Singleton
    static EventDispatcher instance;
    public static EventDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObj = new GameObject();
                singletonObj.name = "EventDispatcher";
                instance = singletonObj.AddComponent<EventDispatcher>();
            }
            return instance;
        }
        private set { }
    }
      void Awake()
    {
        if (instance == null || instance.GetInstanceID() == this.GetInstanceID())
            instance = this;
    }
    #endregion

    Dictionary<EventID, Action<object>> listeners = new Dictionary<EventID, Action<object>>();

    #region Add, Post, Remove

    /// <summary>
    /// Register event by listener
    /// </summary>
    public void RegisterListener(EventID eventID, Action<object> callback)
    {
        if (listeners.ContainsKey(eventID))
        {
            listeners[eventID] += callback;
        }
        else
        {
            listeners.Add(eventID, null);
            listeners[eventID] += callback;
        }
    }

    /// <summary>
    /// Post event
    /// </summary>
    public void PostEvent(EventID eventID, object param = null)
    {
        if (!listeners.ContainsKey(eventID))
            return;

        var callbacks = listeners[eventID];
        if (callbacks != null)
            callbacks(param);
        else
            listeners.Remove(eventID);
    }

    public void RemoveListener(EventID eventID, Action<object> callback)
    {
        if (listeners.ContainsKey(eventID))
            listeners[eventID] -= callback;
    }
    #endregion
}