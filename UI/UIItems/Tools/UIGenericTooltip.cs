using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public abstract class UIGenericTooltip<T> : UITooltip where T : Item
{
    [SerializeField]
    protected T m_Origin;
    public T Origin
    {
        get { return m_Origin; }
    }

    public RectTransform rectTransform
    {
        get { return transform as RectTransform; }
    }

    public virtual void Open(T item)
    {
        m_Origin = item;
        Open();
    }
}
