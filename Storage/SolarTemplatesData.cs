using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
namespace Storage
{
    [Serializable]
    public class SolarTemplatesData : BaseData
    {
        public List<SolarItem> templates = new List<SolarItem>();
    }
}