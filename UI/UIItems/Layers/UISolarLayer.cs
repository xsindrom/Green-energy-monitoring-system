using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Storage;

public class UISolarLayer : UIGenericLayer<UISolarItem, SolarItem>
{

    public override void Save(string key)
    {
        var items = m_UIItems.Select(x => x.Origin).ToList();
        StorageController.Instance.data.solarBaterriesData.Save(key, items);
        RemoveAll();
    }

    public override void Load(string key)
    {
        var items = StorageController.Instance.data.solarBaterriesData.Load(key);

        if (items != null)
        {
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                Create(item);
            }
        }
    }

    public override UISolarItem Create(SolarItem item)
    {
        var uiItem = base.Create(item);
        uiItem.Source = this;
        return uiItem;
    }
}
