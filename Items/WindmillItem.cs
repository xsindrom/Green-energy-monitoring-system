using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class WindmillItem : Item
    {
        public float efficiency;
        public float radius;
        public float heightWindmill;
        public float windSpeed; 
        public float pressure;
        public float temperature;
        public Vector2 position;
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
                coordinates = value;
            }
        }

        public WindmillItem()
        {
            isInit = false;
        }

        public WindmillItem(Vector2 coordinates)
        {
            this.coordinates = coordinates;
        }

        public static WindmillItem Copy(WindmillItem item)
        {
            var copied = new WindmillItem()
            {
                efficiency = item.efficiency,
                radius = item.radius,
                heightWindmill = item.heightWindmill,
                windSpeed = item.windSpeed,
                pressure = item.pressure,
                temperature = item.temperature,
                position = item.position,
                time = item.time
            };
            return copied;
        }
    }
}