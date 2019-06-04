using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public abstract class UIGenericLayer<T, U>: UILayer where T: UIItem<U> where U: Item 
{
    [SerializeField]
    protected T m_Template;
    [SerializeField]
    protected List<T> m_UIItems = new List<T>();

    public List<T> UIItems
    {
        get { return m_UIItems; }
    }

    public T Template
    {
        get { return m_Template; }
    }

    public virtual T Create(U item)
    {
        var uiItem = Instantiate(m_Template, root);
        uiItem.Init(item, m_MinCoords, m_MaxCoords);
        uiItem.gameObject.SetActive(true);
        m_UIItems.Add(uiItem);
        return uiItem;
    }

    public virtual void CreateAll(List<U> items)
    {
        for(int i = 0; i < items.Count; i++)
        {
            var item = items[i];
            Create(item);
        }
    }

    public virtual void Remove(T item)
    {
        if (m_UIItems.Contains(item))
        {
            m_UIItems.Remove(item);
            Destroy(item.gameObject);
        }
    }

    public virtual void RemoveAll()
    {
        for(int i = 0; i < m_UIItems.Count; i++)
        {
            var item = m_UIItems[i];
            Destroy(item.gameObject);
        }
        m_UIItems.Clear();
    }
}
