using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITable : MonoBehaviour
{
    [SerializeField]
    protected UITableRow m_Headers;
    [SerializeField]
    protected RectTransform m_ContentRoot;
    [SerializeField]
    protected UITableRow m_RowTemplate;
    [SerializeField]
    protected List<UITableRow> m_Rows = new List<UITableRow>();

    public UITableRow Headers
    {
        get { return m_Headers; }
    }

    public RectTransform ContentRoot
    {
        get { return m_ContentRoot; }
    }

    public List<UITableRow> Rows
    {
        get { return m_Rows; }
    }


    public UITableRow CreateRow()
    {
        var cloned = Instantiate(m_RowTemplate, m_ContentRoot);
        cloned.gameObject.SetActive(true);
        m_Rows.Add(cloned);
        return cloned;
    }

    public void Clear()
    {
        for(int i = 0; i < m_Rows.Count; i++)
        {
            var row = m_Rows[i];
            Destroy(row.gameObject);
        }
        m_Rows.Clear();
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        for(int i =0; i < m_Headers.Columns.Count; i++)
        {
            builder.Append(m_Headers.Columns[i].text);
            builder.Append(';');
        }
        
        for(int i = 0; i < m_Rows.Count; i++)
        {
            var row = m_Rows[i];
            builder.Append('\n');
            for(int j  = 0; j < row.Columns.Count; j++)
            {
                builder.Append(row.Columns[j].text);
                builder.Append(';');
            }
        }
        return builder.ToString();
    }
}
