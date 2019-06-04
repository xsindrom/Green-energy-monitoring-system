using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network.GoogleTables
{
    [Serializable]
    public class GoogleTableResponce : BaseResponce
    {
        public GoogleTable table;

        public override T Parse<T>(string json)
        {
            var parsedResponce = new GoogleTableResponce()
            {
                table = GoogleTable.Parse(json)
            };
            return parsedResponce as T;
        }
    }
}