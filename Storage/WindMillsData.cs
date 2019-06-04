using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Storage
{
    [Serializable]
    public class WindMillsData : BaseData
    {
        [Serializable]
        public class SWindData
        {
            public string key;
            public List<WindmillItem> items = new List<WindmillItem>();
        }

        public List<SWindData> windMills = new List<SWindData>();

        public void Save(string key, List<WindmillItem> items)
        {
            SWindData sData = StorageController.Instance.data.windMillsData.windMills.Find(x => x.key == key);
            if (sData == null)
            {
                sData = new SWindData()
                {
                    key = key,
                    items = items
                };
                StorageController.Instance.data.windMillsData.windMills.Add(sData);
            }
            else
            {
                sData.items = items;
            }
        }

        public List<WindmillItem> Load(string key)
        {
            var sData = windMills.Find(x => x.key == key);
            if (sData == null) { return null; }

            return sData.items;
        }
    }
}
