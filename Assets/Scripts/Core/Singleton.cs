using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T s_Instance;

    public virtual void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this as T;
        }
        else
        {
            Destroy(this);
        }
    }
}