using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class RegionItem : Item
    {
        public List<CityItem> cities = new List<CityItem>();
        public List<MeteoCenterItem> meteos = new List<MeteoCenterItem>();
    }
}