using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    [Serializable]
    public class BaseRequest
    {
        public string path;
        public string postData;
    }
}