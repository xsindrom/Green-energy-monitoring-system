using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Storage;

public class UISolarToolsContainer : UIToolsContainer
{
    [SerializeField]
    protected UISolarDragableObject m_Template;
    [SerializeField]
    protected List<UISolarDragableObject> tools = new List<UISolarDragableObject>();

    public UISolarDragableObject Template
    {
        get { return m_Template; }
    }

    public List<UISolarDragableObject> Tools
    {
        get { return tools; }
    }

    public override void Save()
    {
        base.Save();
        var templates = StorageController.Instance.data.solarTemplatesData.templates;
        for(int i = 0; i < Tools.Count; i++)
        {
            var toolsTemplate = Tools[i].OriginTemplate;
            if (!templates.Contains(toolsTemplate))
            {
                templates.Add(toolsTemplate);
            }
        }
    }

    public override void Load()
    {
        base.Load();
        var templates = StorageController.Instance.data.solarTemplatesData.templates;
        for (int i = 0; i < templates.Count; i++) 
        {
            var template = templates[i];
            AddTool(template);
        }
    }

    public void AddTool(SolarItem item)
    {
        var solar = Tools.Find(x => x.OriginTemplate == item);
        if(solar == null)
        {
            var clonedTool = Instantiate(m_Template, root);
            clonedTool.targetObject = null;
            clonedTool.OriginTemplate = item;
            Tools.Add(clonedTool);
        }
    }
}
