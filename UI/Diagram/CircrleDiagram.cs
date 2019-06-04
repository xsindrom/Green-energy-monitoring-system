using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircrleDiagram : Diagram
{
    [SerializeField]
    private RectTransform root;
    [SerializeField]
    private UIDiagramPart template;

    [ContextMenu("Draw")]
    public override void Draw()
    {
        m_uiParts.Clear();
        for (int i = 0; i < root.childCount; i++)
        {
            var child = root.GetChild(i);
            Destroy(child.gameObject);
        }

        var generalValue = 0.0f;
        for (int i = 0; i < m_parts.Count; i++)
        {
            var part = m_parts[i];
            generalValue += part.value;
        }
        m_parts.Sort(new DiagramPartComparer());

        var fillValue = 0.0f;
        for(int i = 0; i < m_parts.Count; i++)
        {
            var part = m_parts[i];
            part.percentage = part.value / generalValue;
            fillValue += part.percentage;

            var uiPart = Instantiate(template, root);
            uiPart.Init(fillValue, part);
            m_uiParts.Add(uiPart);
        }
    }
}