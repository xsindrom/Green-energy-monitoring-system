using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Storage
{
    [Serializable]
    public class SolarBaterriesData : BaseData
    {
        [Serializable]
        public class SSolarData
        {
            public string key;
            public List<SolarItem> items = new List<SolarItem>();
        }

        public List<SSolarData> solarBatteries = new List<SSolarData>();

        public void Save(string key, List<SolarItem> items)
        {
            SSolarData sData = StorageController.Instance.data.solarBaterriesData.solarBatteries.Find(x => x.key == key);
            if (sData == null)
            {
                sData = new SSolarData()
                {
                    key = key,
                    items = items
                };
                StorageController.Instance.data.solarBaterriesData.solarBatteries.Add(sData);
            }
            else
            {
                sData.items = items;
            }
        }

        public List<SolarItem> Load(string key)
        {
            var sData = solarBatteries.Find(x => x.key == key);
            if (sData == null) { return null; }

            return sData.items;
        }
    }
}