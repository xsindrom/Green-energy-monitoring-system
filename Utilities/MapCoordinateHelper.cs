using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class MapCoordinateHelper
{
    public static Vector2 ConvertToCoordinates(Vector2 minCoordinates, Vector2 maxCoordinates, Vector2 uiPosition)
    {
        var uiScale = new Vector2((maxCoordinates - minCoordinates).x / Screen.width,
                                  (maxCoordinates - minCoordinates).y / Screen.height);
        var position = Vector2.Scale(uiPosition, uiScale) + minCoordinates;
        return position;
    }

    public static Vector2 ConvertToCoordinates(Vector2 minCoordinates, Vector2 maxCoordinates, Vector2 uiPosition, Rect uiRect)
    {
        if (uiRect == default(Rect))
        {
            return ConvertToCoordinates(minCoordinates, maxCoordinates, uiPosition);
        }
        var uiScale = new Vector2((maxCoordinates - minCoordinates).x / uiRect.width,
                                  (maxCoordinates - minCoordinates).y / uiRect.height);
        var position = Vector2.Scale(uiPosition, uiScale) + minCoordinates;
        return position;
    }

    public static Vector2 ConvertToUI(Vector2 minCoordinates, Vector2 maxCoordinates, Vector2 coordPosition)
    {
        var uiScale = new Vector2(Screen.width / (maxCoordinates - minCoordinates).x,
                                  Screen.height / (maxCoordinates - minCoordinates).y);
        var position = Vector2.Scale(coordPosition - minCoordinates, uiScale);
        return position;
    }

    public static Vector2 ConvertToUI(Vector2 minCoordinates, Vector2 maxCoordinates, Vector2 coordPosition, Rect coordRect)
    {
        if (coordRect == default(Rect))
        {
            return ConvertToUI(minCoordinates, maxCoordinates, coordPosition);
        }
        var uiScale = new Vector2(coordRect.width / (maxCoordinates - minCoordinates).x,
                                  coordRect.height / (maxCoordinates - minCoordinates).y);
        var position = Vector2.Scale(coordPosition - minCoordinates, uiScale);
        return position;
    }

    public static Vector2 ConvertMousePositionToUI(Vector2 mousePosition)
    {
        var position = ConvertToCoordinates(new Vector2(-Screen.width / 2, -Screen.height / 2),
                                            new Vector2(Screen.width / 2, Screen.height / 2),
                                            mousePosition);
        return position;
    }

    public static Vector2 ConvertToSingle(Vector2 input)
    {
        Vector2Int inputInt = Vector2Int.FloorToInt(input);
        var minutes = (input - inputInt) * 100f / 60f;
        return inputInt + minutes;
    }

    public static Vector2 ConvertToMinute(Vector2 input)
    {
        Vector2Int inputInt = Vector2Int.FloorToInt(input);

        var minutes = (input - inputInt) * 60f / 100f;

        return inputInt + minutes;
    }

    public static Vector2 GetUIOffset()
    {
        return new Vector2(Screen.width / 2, Screen.height / 2);
    }

    public static Vector2 GetScaleFactor()
    {
        var referenceResolution = UIMainController.Instance.Scaler.referenceResolution;
        return new Vector2(referenceResolution.x / Screen.width, referenceResolution.y / Screen.height);
    }

    /// <summary>
    /// Source code of Unity UI
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public static Rect GetRectOfPreserveAspect(this Image image)
    {
        var size = image.sprite.rect.size;
        var r = image.rectTransform.rect;

        if (image.preserveAspect && size.sqrMagnitude > 0.0f)
        {
            var spriteRatio = size.x / size.y;
            var rectRatio = image.rectTransform.sizeDelta.x / image.rectTransform.sizeDelta.y;

            if (spriteRatio > rectRatio)
            {
                var oldHeight = r.height;
                r.height = r.width * (1.0f / spriteRatio);
                r.y += (oldHeight - r.height) * image.rectTransform.pivot.y;
            }
            else
            {
                var oldWidth = r.width;
                r.width = r.height * spriteRatio;
                r.x += (oldWidth - r.width) * image.rectTransform.pivot.x;
            }
        }
        var imageOldRect = image.rectTransform.rect.size;
        if (r.width > imageOldRect.x)
        {
            var scaleX = 1 - (r.width - imageOldRect.x) / imageOldRect.x;
            r.width *= scaleX;
            r.height *= scaleX;
        }
        return r;
    }
}
