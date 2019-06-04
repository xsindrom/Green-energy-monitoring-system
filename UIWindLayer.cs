using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Storage;

public class UIWindLayer : UIGenericLayer<UIWindmillItem, WindmillItem>
{
    public override void Save(string key)
    {
        var items = m_UIItems.Select(x => x.Origin).ToList();
        StorageController.Instance.data.windMillsData.Save(key, items);

        RemoveAll();
    }

    public override void Load(string key)
    {
        var items = StorageController.Instance.data.windMillsData.Load(key);

        if (items != null)
        {
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                Create(item);
            }
        }
    }
    public override UIWindmillItem Create(WindmillItem item)
    {
        var uiItem = base.Create(item);
        uiItem.Source = this;
        return uiItem;
    }
}
