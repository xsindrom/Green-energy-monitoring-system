using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITableRow : MonoBehaviour
{
    [SerializeField]
    protected List<Text> m_Columns = new List<Text>();

    public List<Text> Columns
    {
        get { return m_Columns; }
    }

    public Text this[string id]
    {
        get { return m_Columns.Find(x => x.name == id); }
    }
}
