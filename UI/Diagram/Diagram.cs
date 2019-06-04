using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Diagram : MonoBehaviour
{
    [SerializeField]
    protected List<DiagramPart> m_parts = new List<DiagramPart>();
    public List<DiagramPart> Parts
    {
        get
        {
            return m_parts;
        }
    }

    protected List<UIDiagramPart> m_uiParts = new List<UIDiagramPart>();
    public List<UIDiagramPart> UIParts
    {
        get
        {
            return m_uiParts;
        }
    }

    public virtual void Init(List<float> values)
    {
        m_parts.Clear();
        for(int i = 0; i < values.Count; i++)
        {
            var m_part = new DiagramPart()
            {
                value = values[i]
            };
            m_parts.Add(m_part);
        }
    }

    public virtual void Draw()
    {

    }
}