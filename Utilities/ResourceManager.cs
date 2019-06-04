using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    private static Dictionary<string, Object> cache = new Dictionary<string, Object>();
    public static Items.ItemsSettings GetItemsSettings()
    {
        if (!cache.ContainsKey(SettingsKeys.PATH_ITEM_SETTINGS))
        {
            var settings = Resources.Load(SettingsKeys.PATH_ITEM_SETTINGS);
            cache.Add(SettingsKeys.PATH_ITEM_SETTINGS, settings);
            return settings as Items.ItemsSettings;
        }
        else
        {
            var settings = cache[SettingsKeys.PATH_ITEM_SETTINGS];
            return settings as Items.ItemsSettings;
        }
    }
    public static SpriteContainer GetSpriteContainer(string name)
    {
        const string SPRITE_CONTAINER = "spriteContainer_";
        var pathToContainer = SPRITE_CONTAINER + name;
        if (!cache.ContainsKey(pathToContainer))
        {
            var path = SettingsKeys.PATH_SPRITE_CONTAINERS + "/" + name;
            var container = Resources.Load(path);
            cache.Add(pathToContainer, container);
            return container as SpriteContainer;
        }
        else
        {
            var container = cache[pathToContainer];
            return container as SpriteContainer;
        }
    }
}
