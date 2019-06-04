using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void SwitchState(this GameObject gameObject)
    {
        if(gameObject)
            gameObject.SetActive(!gameObject.activeSelf);
    }
}
