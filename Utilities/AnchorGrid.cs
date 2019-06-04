using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnchorGrid
{
    [SerializeField]
    private int rows;
    [SerializeField]
    private int columns;

    public int Rows
    {
        get { return rows; }
    }

    public int Columns
    {
        get { return columns; }
    }

    public AnchorGrid()
    {
        rows = 1;
        columns = 1;
    }

    public AnchorGrid(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
    }

    public List<Anchor> GetAnchors()
    {
        var anchors = new List<Anchor>();
        var dX = 1f / Columns;
        var dY = 1f / Rows;

        for (int i = 0; i < Rows; i++)
        {
            var yMin = i * dY;
            var yMax = (i + 1) * dY;

            for (int j = 0; j < Columns; j++)
            {
                var xMin = j * dX;
                var xMax = (j + 1) * dX;

                anchors.Add(new Anchor(xMin, yMin, xMax, yMax));
            }
        }
        return anchors;
    }
}

[Serializable]
public class Anchor
{
    public Vector2 min;
    public Vector2 max;

    public Anchor()
    {
        min = Vector2.zero;
        max = Vector2.one;
    }

    public Anchor(Vector2 min, Vector2 max)
    {
        this.min = min;
        this.max = max;
    }

    public Anchor(float minX, float minY, float maxX, float maxY)
    {
        min = new Vector2(minX, minY);
        max = new Vector2(maxX, maxY);
    }
}