using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;
using Storage;
public class UIRegionMapWindow : UIBaseWIndow
{
    protected ItemsSettings settings;
    protected SpriteContainer spriteContainer;

    protected Vector2 countryMinCoord;
    protected Vector2 countryMaxCoord;

    protected string regionName;
    protected Vector2 regionCoord;
    protected Vector2 regionMinCoord;
    protected Vector2 regionMaxCoord;

    [SerializeField]
    protected Image mainImage;
    [SerializeField]
    protected GameObject toolsBar;
    [SerializeField]
    protected UISolarDragableObject solarDragObject;
    [SerializeField]
    protected UIWindmillDragableObject windmillDragObject;
    [SerializeField]
    protected UILayerController layerController;
    [SerializeField]
    protected UIToolsPanel toolsPanel;
    [SerializeField]
    protected UITooltipContainer tooltips;

    public UILayerController LayerController
    {
        get { return layerController; }
    }

    public UIToolsPanel ToolsPanel
    {
        get { return toolsPanel; }
    }

    public UITooltipContainer Tooltips
    {
        get { return tooltips; }
    }


    public override void Init()
    {
        base.Init();
        settings = ResourceManager.GetItemsSettings();
        spriteContainer = ResourceManager.GetSpriteContainer("RegionContainer");

        var country = settings.countries[0];
        countryMinCoord = country.min;
        countryMaxCoord = country.max;
    }

    public void OpenWindow(UIRegionItem uiRegion)
    {
        OpenWindow();
        
        var region = uiRegion.Origin;
        regionName = region.name;
        mainImage.sprite = spriteContainer.GetSprite(region.name) ?? uiRegion.baseImage.sprite;
        var r = mainImage.GetRectOfPreserveAspect();

        regionCoord = region.Coordinates;
        var uiCenter = MapCoordinateHelper.ConvertToUI(countryMinCoord, countryMaxCoord, regionCoord, r);
        regionMinCoord = MapCoordinateHelper.ConvertToCoordinates(countryMinCoord, countryMaxCoord, uiCenter - uiRegion.BaseRect.sizeDelta / 2, r);
        regionMaxCoord = MapCoordinateHelper.ConvertToCoordinates(countryMinCoord, countryMaxCoord, uiCenter + uiRegion.BaseRect.sizeDelta / 2, r);

        var coordinatesWindow = UIMainController.Instance.GetWindow<UIMapCoordinatesWindow>(UIConstants.WINDOW_MAP_COORDINATES);
        coordinatesWindow.OpenWindow(regionMinCoord, regionMaxCoord);

        layerController.LoadLayers(regionName, regionMinCoord, regionMaxCoord);
    }

    public override void CloseWindow()
    {
        base.CloseWindow();
        layerController.UnloadLayers();
        toolsBar.SetActive(false);
        var countryWindow = UIMainController.Instance.GetWindow<UICountryMapWindow>(UIConstants.WINDOW_COUNTRY_MAP);
        countryWindow.OpenWindow();
    }
}
