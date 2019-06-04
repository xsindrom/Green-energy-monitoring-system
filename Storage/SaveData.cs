using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Storage
{
    [Serializable]
    public class SaveData : BaseData
    {
        public SolarBaterriesData solarBaterriesData = new SolarBaterriesData();
        public WindMillsData windMillsData = new WindMillsData();
        public SolarTemplatesData solarTemplatesData = new SolarTemplatesData();
        public WindTemplatesData windTemplatesData = new WindTemplatesData();
    }
}