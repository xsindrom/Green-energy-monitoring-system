using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    [Serializable]
    public class BaseResponce
    {
        public Status status;

        public virtual T Parse<T>(string json) where T : BaseResponce
        {
            return JsonUtility.FromJson<T>(json);
        }

    }

    public enum Status { OK, ERROR}
}