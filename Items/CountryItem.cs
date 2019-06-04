using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class CountryItem : Item
    {
        public Vector2 min;
        public Vector2 max;
        public List<RegionItem> regions = new List<RegionItem>();
    }
}