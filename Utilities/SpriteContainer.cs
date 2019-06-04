using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/SpriteContainer")]
public class SpriteContainer : ScriptableObject
{
    public int width;
    public int height;
    public string spritesFolder;
    public List<SpriteContainerItem> spriteItems = new List<SpriteContainerItem>();

    public Sprite GetSprite(string name)
    {
        var spriteItem = spriteItems.Find(x => x.name == name);
        if(spriteItem != null)
        {
            return spriteItem.sprite;
        }
        return null;
    }

    [ContextMenu("FillSprites")]
    public void FillSprites()
    {
        spriteItems.Clear();
        var sprites = Resources.LoadAll<Sprite>(spritesFolder);
        for(int i = 0; i < sprites.Length; i++)
        {
            var sprite = sprites[i];
            spriteItems.Add(new SpriteContainerItem()
            {
                name = sprite.name,
                sprite = sprite
            });

        }
    }

    #region hardcode util
    [ContextMenu("Find Coordinates")]
    public void FindCoordinates()
    {
        const float width = 2770;
        const float height = 1847;

        Vector2 min = new Vector2(22.15f, 44.38334f);
        Vector2 max = new Vector2(40.18333f, 52.333f);

        Vector2 position = new Vector2(226f, 102f);
        Vector2 size = new Vector2(384f, 411f);

        var mapRect = new Rect(0, 0, width, height);
        var uiPosition = new Vector2(position.x + size.x / 2, height - (position.y + size.y / 2));
        var coords = MapCoordinateHelper.ConvertToCoordinates(min, max, uiPosition, mapRect);
        Debug.Log(MapCoordinateHelper.ConvertToSingle(coords).ToString("F4"));

    }
    #endregion
}
[Serializable]
public class SpriteContainerItem
{
    public string name;
    public Sprite sprite;
}