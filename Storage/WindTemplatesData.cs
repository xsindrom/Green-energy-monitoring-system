using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Storage
{
    [Serializable]
    public class WindTemplatesData : BaseData
    {
        public List<WindmillItem> templates = new List<WindmillItem>();
    }
}