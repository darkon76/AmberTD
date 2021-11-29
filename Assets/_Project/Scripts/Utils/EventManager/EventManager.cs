using System;
using System.Collections.Generic;

public static class EventManager
{
    private static readonly Dictionary<int, Action> _eventActionDictionary = new Dictionary<int, Action>();
    private static readonly Dictionary<int, EventArguments> _eventParametersDictionary = new Dictionary<int, EventArguments>();
    public delegate void EventArguments(object[] arg);

    #region SingleAction

    /// <summary>
    /// Starts the listening an event.
    /// Its best to be called OnEnabled
    /// </summary>
    /// <param name="event">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void Listen(eEvent @event, Action listener)
    {
        var eventId = (int)@event;
        if (!_eventActionDictionary.ContainsKey(eventId))
        {
            _eventActionDictionary.Add(eventId, listener);
        }
        else
        {
            _eventActionDictionary[eventId] += listener;
        }
    }

    /// <summary>
    /// Stops the listening an Event.
    /// Its best to be called OnDisabled
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void Remove(eEvent eventName, Action listener)
    {
        _eventActionDictionary[(int)eventName] -= listener;
    }

    /// <summary>
    /// Triggers the event.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="args">Arguments.</param>
    public static void Trigger(eEvent eventName)
    {
        if (!_eventActionDictionary.TryGetValue((int)eventName, out var action))
        {
            return;
        }
        action?.Invoke();
    }

    #endregion

    #region SingleAction

    /// <summary>
    /// Starts the listening an event.
    /// Its best to be called OnEnabled
    /// </summary>
    /// <param name="event">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void Listen(eEvent @event, EventArguments listener)
    {
        var eventId = (int)@event;
        if (!_eventParametersDictionary.ContainsKey(eventId))
        {
            _eventParametersDictionary.Add(eventId, listener);
        }
        else
        {
            _eventParametersDictionary[eventId] += listener;
        }
    }

    /// <summary>
    /// Stops the listening an Event.
    /// Its best to be called OnDisabled
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void Remove(eEvent eventName, EventArguments listener)
    {
        _eventParametersDictionary[(int)eventName] -= listener;
    }

    /// <summary>
    /// Triggers the event.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="args">Arguments.</param>
    public static void Trigger(eEvent eventName, params object[] args)
    {
        if (!_eventParametersDictionary.TryGetValue((int)eventName, out var action))
        {
            return;
        }
        action?.Invoke(args);
    }

    #endregion
}