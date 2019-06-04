using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHelper : MonoSingleton<EventHelper>
{
    public static void SafeCall<T>(UnityAction<T> callback, T param)
    {
        if(callback != null)
        {
            callback.Invoke(param);
        }
    }

    public static void SafeCall(UnityAction callback)
    {
        if(callback != null)
        {
            callback.Invoke();
        }
    }

    public static void SafeCall<T>(UnityEvent<T> callback, T param)
    {
        if (callback != null)
        {
            callback.Invoke(param);
        }
    }

    public static void SafeCall<T,U>(UnityEvent<T,U> callback, T param0, U param1)
    {
        if(callback != null)
        {
            callback.Invoke(param0, param1);
        }
    }


    public static void SafeCall(UnityEvent callback)
    {
        if(callback != null)
        {
            callback.Invoke();
        }
    }

    public static void SafeCall(IEnumerator coroutine)
    {
        if(coroutine != null)
        {
            Instance.StartCoroutine(coroutine);
        }
    }

}
