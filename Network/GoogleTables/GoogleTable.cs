using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace Network.GoogleTables
{
    [Serializable]
    public class GoogleTable
    {
        public string range;
        public MajorDimension majorDimension;
        public List<GoogleTableRow> rows = new List<GoogleTableRow>();
        public List<GoogleTableColumn> columns = new List<GoogleTableColumn>();

        public static GoogleTable Parse(string json)
        {
            var table = new GoogleTable();

            JSONNode jNode = JSONNode.Parse(json);
            table.range = jNode["range"];
            table.majorDimension = (MajorDimension)Enum.Parse(typeof(MajorDimension), jNode["majorDimension"]);
            var valuesNode = jNode["values"];

            if (table.majorDimension == MajorDimension.COLUMNS)
            {
                for (int i = 0; i < valuesNode.Count; i++)
                {
                    var columnNode = valuesNode[i];
                    var column = new GoogleTableColumn();
                    for (int j = 0; j < columnNode.Count; j++)
                    {
                        var value = columnNode[j];
                        column.rows.Add(value);
                    }
                    table.columns.Add(column);
                }
            }
            else
            {
                for (int i = 0; i < valuesNode.Count; i++)
                {
                    var rowNode = valuesNode[i];
                    var row = new GoogleTableRow();
                    for (int j = 0; j < rowNode.Count; j++)
                    {
                        var value = rowNode[j];
                        row.columns.Add(value);
                    }
                    table.rows.Add(row);
                }
            }
            return table;
        }
    }

    [Serializable]
    public class GoogleTableRow
    {
        public List<string> columns = new List<string>();
    }

    [SerializeField]
    public class GoogleTableColumn
    {
        public List<string> rows = new List<string>();
    }
}