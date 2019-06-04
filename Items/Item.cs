using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [Serializable]
    public abstract class Item
    {
        public string name;
        [SerializeField]
        protected Vector2 coordinates;

        [NonSerialized]
        protected bool isInit;
        [NonSerialized]
        protected Vector2 m_Coordinates;
        public virtual Vector2 Coordinates
        {
            get
            {
                if (!isInit)
                {
                    m_Coordinates = coordinates;
                    isInit = true;
                }
                return m_Coordinates;
            }
            set { m_Coordinates = value; }
        }
    }
}