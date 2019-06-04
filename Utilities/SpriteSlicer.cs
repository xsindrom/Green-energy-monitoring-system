using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteSlicer
{
    private const string NAME_FORMAT = "{0}_{1}_{2}";
    public static List<Sprite> GetSprites(this Sprite sprite, int rows, int columns)
    {
        Texture2D texture = sprite.texture;
        int width = texture.width / columns;
        int height = texture.height / rows;
        
        var sprites = new List<Sprite>();
        for (int i = 0; i < rows; i++)
        {
            int y = i * height;
            for (int j = 0; j < columns; j++)
            {
                int x = j * width;

                Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false, false);
                var pixels = texture.GetPixels(x, y, width, height);
                tex.SetPixels(pixels);
                tex.Apply();

                Sprite spr = Sprite.Create(tex, new Rect(0, 0, width, height), Vector2.one * 0.5f);
                spr.name = string.Format(NAME_FORMAT, sprite.name, i, j);
                sprites.Add(spr);
            }
        }
        return sprites;
    }
}