using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class UIMeteoLayer : UIGenericLayer<UIMeteoCenterItem, MeteoCenterItem>
{
    private ItemsSettings m_Settings;

    public override void Save(string key)
    {
        base.Save(key);
        RemoveAll();
    }

    public override void Load(string key)
    {
        base.Load(key);
        m_Settings = ResourceManager.GetItemsSettings();
        var region = m_Settings.countries[0].regions.Find(x => x.name == key);
        if (region != null)
        {
            var items = region.meteos;
            CreateAll(items);
        }
    }
}
