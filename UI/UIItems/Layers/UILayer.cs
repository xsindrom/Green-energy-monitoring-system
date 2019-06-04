using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class UILayer : MonoBehaviour {

    [SerializeField]
    protected RectTransform root;
    [SerializeField]
    protected Vector2 m_MinCoords;
    [SerializeField]
    protected Vector2 m_MaxCoords;

    public RectTransform Root
    {
        get { return root; }
    }

    public Vector2 MinCoords
    {
        get { return m_MinCoords; }
        set { m_MinCoords = value; }
    }

    public Vector2 MaxCoords
    {
        get { return m_MaxCoords; }
        set { m_MaxCoords = value; }
    }

    public virtual void Init(Vector2 minCoords, Vector2 maxCoords)
    {
        MinCoords = minCoords;
        MaxCoords = maxCoords;
    }

    public virtual void Save(string key)
    {

    }

    public virtual void Load(string key)
    {

    }
}
