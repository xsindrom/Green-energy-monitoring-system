using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapCoordinatesWindow : UIBaseWIndow
{
    public Text mousePosition;
    private Vector2 min;
    private Vector2 max;
    private Rect mapRect;

    public override void Init()
    {
        base.Init();
        var settings = ResourceManager.GetItemsSettings();
        var country = settings.countries[0];
        min = country.min;
        max = country.max;
    }

    public void OpenWindow(Vector2 min, Vector2 max, Rect mapRect = default(Rect))
    {
        this.min = min;
        this.max = max;
        this.mapRect = mapRect;
        OpenWindow();
    }

    protected override void Update()
    {
        base.Update();
        var position = MapCoordinateHelper.ConvertToCoordinates(min,max, Input.mousePosition, mapRect);
        mousePosition.text = position.ToString("F2") + " | " + MapCoordinateHelper.ConvertToMinute(position).ToString("F2");
    }
}
