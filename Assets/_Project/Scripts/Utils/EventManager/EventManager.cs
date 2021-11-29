using System;
using System.Collections.Generic;

public static class EventManager
{
    private static readonly EventArguments[] _eventDictionary = new EventArguments[(int)eEvent.MAX];
    public delegate void EventArguments();

    /// <summary>
    /// Starts the listening an event.
    /// Its best to be called OnEnabled
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void Listen(eEvent eventName, EventArguments listener)
    {
        _eventDictionary[(int)eventName] += listener;
    }

    /// <summary>
    /// Stops the listening an Event.
    /// Its best to be called OnDisabled
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void Remove(eEvent eventName, EventArguments listener)
    {
        _eventDictionary[(int)eventName] -= listener;
    }

    /// <summary>
    /// Triggers the event.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="args">Arguments.</param>
    public static void Trigger(eEvent eventName)
    {
        if (_eventDictionary[(int)eventName] != null)
        {
            _eventDictionary[(int) eventName]();
        }
    }
}