using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class SolarItem : Item
    {
        public float angleToSouth;
        public float angleToHorizont;
        public float efficiency;
        public float power;
        public float square;
        public float Qs;
        public float Qd;
        public float cloudiness;
        public DateTime time;

        public override Vector2 Coordinates
        {
            get
            {
                return base.Coordinates;
            }
            set
            {
                m_Coordinates = value;
                coordinates = m_Coordinates;
            }
        }

        public SolarItem()
        {
            isInit = false;
        }

        public SolarItem(Vector2 coordinates)
        {
            this.coordinates = coordinates;
        }

        public static SolarItem Copy(SolarItem item)
        {
            var copied = new SolarItem()
            {
                angleToSouth = item.angleToSouth,
                angleToHorizont = item.angleToHorizont,
                efficiency = item.efficiency,
                power = item.power,
                square = item.square,
                Qs = item.Qs,
                Qd = item.Qd,
                cloudiness = item.cloudiness,
                time = item.time
            };
            return copied;
        }
    }
}