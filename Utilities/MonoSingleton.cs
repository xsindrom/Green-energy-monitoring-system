using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object _object = new object();
    private static T instance;

    public static T Instance
    {
        get
        {
            lock (_object)
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<T>();

                    var objects = FindObjectsOfType<T>();
                    if(objects.Length > 1)
                    {
                        for(int i = 1; i < objects.Length; i++)
                        {
                            var obj = objects[i];
                            Destroy(obj.gameObject);
                        }
                        return instance;
                    }
                    if(instance == null)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).ToString();
                        DontDestroyOnLoad(singleton);
                    }
                }
                return instance;
            }
        }
    }

    public static bool HasInstance
    {
        get
        {
            return instance != null;
        }
    }

    public static void CreateInstance(bool dontDestroyOnLoad)
    {
        if (!HasInstance)
        {
            GameObject singleton = new GameObject();
            instance = singleton.AddComponent<T>();
            singleton.name = typeof(T).ToString();
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(singleton);
            }
        }
    }

    public virtual void Init()
    {

    }

    protected virtual void Awake()
    {
        Init();
    }
}
