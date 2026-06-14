using System;
using System.Collections.Generic;
using UnityEngine;

public class AimEventDispatcher : MonoBehaviour
{
    // イベント登録（引数を受け取れるようにする）
    private static Dictionary<string, Action<object[]>> eventTable = new();

    public static void Subscribe(string eventName, Action<object[]> listener)
    {
        if (!eventTable.ContainsKey(eventName))
            eventTable[eventName] = null;
        eventTable[eventName] += listener;
    }

    public static void Unsubscribe(string eventName, Action<object[]> listener)
    {
        if (eventTable.ContainsKey(eventName))
            eventTable[eventName] -= listener;
    }

    // パラメータ付きイベント発火
    public static void Fire(string eventName, params object[] args)
    {
        if (eventTable.TryGetValue(eventName, out var action))
        {
            Debug.Log($"Fire Event: {eventName} (args: {args.Length})");
            action?.Invoke(args);
        }
        else
        {
            Debug.LogWarning($"Event '{eventName}' not found!");
        }
    }
}
