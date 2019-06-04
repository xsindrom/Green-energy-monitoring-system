using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Storage;
using Items;

public class UIWindToolsContainer : UIToolsContainer
{
    [SerializeField]
    protected UIWindmillDragableObject m_Template;
    [SerializeField]
    protected List<UIWindmillDragableObject> tools = new List<UIWindmillDragableObject>();

    public UIWindmillDragableObject Template
    {
        get { return m_Template; }
    }

    public List<UIWindmillDragableObject> Tools
    {
        get { return tools; }
    }

    public override void Save()
    {
        base.Save();
        var templates = StorageController.Instance.data.windTemplatesData.templates;
        for (int i = 0; i < Tools.Count; i++)
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
        var templates = StorageController.Instance.data.windTemplatesData.templates;
        for (int i = 0; i < templates.Count; i++)
        {
            var template = templates[i];
            AddTool(template);
        }
    }

    public void AddTool(WindmillItem item)
    {
        var wind = Tools.Find(x => x.OriginTemplate == item);
        if (wind == null)
        {
            var clonedTool = Instantiate(m_Template, root);
            clonedTool.targetObject = null;
            clonedTool.OriginTemplate = item;
            Tools.Add(clonedTool);
        }
    }
}
