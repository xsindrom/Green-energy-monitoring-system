using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToolsPanel : MonoBehaviour {

    [SerializeField]
    protected List<UIToolsContainer> toolsContainers = new List<UIToolsContainer>();

    public List<UIToolsContainer> ToolsContainers
    {
        get { return toolsContainers; }
    }

    public T GetToolsContainer<T>() where T : UIToolsContainer
    {
        var template = toolsContainers.Find(x => x is T);
        if (template)
        {
            return template as T;
        }
        return null;
    }

    public T GetToolsContainer<T>(int id) where T : UIToolsContainer
    {
        var tempToolsContainer = toolsContainers.FindAll(x => x is T);
        if (id < tempToolsContainer.Count)
        {
            return tempToolsContainer[id] as T;
        }
        return null;
    }


    public virtual void SaveAll()
    {
        for(int i = 0; i < toolsContainers.Count; i++)
        {
            var toolsContainer = toolsContainers[i];
            toolsContainer.Save();
        }
    }

    public virtual void LoadAll()
    {
        for(int i = 0; i < toolsContainers.Count; i++)
        {
            var toolsContainer = toolsContainers[i];
            toolsContainer.Load();
        }
    }
  
    public virtual void Show()
    {
        gameObject.SetActive(false);
        LoadAll();
    }

    public virtual void Hide()
    {
        gameObject.SetActive(true);
        SaveAll();
    }
}
