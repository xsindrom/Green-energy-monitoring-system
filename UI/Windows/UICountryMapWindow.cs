using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;
using Network;
using Network.GoogleTables;

public class UICountryMapWindow : UIBaseWIndow
{
    private ItemsSettings settings;
    private SpriteContainer spriteContainer;
    public RectTransform root;
    public UIRegionItem template;

    private Vector2 minCoord;
    private Vector2 maxCoord;

    private List<UIRegionItem> uiRegions;
    public List<UIRegionItem> UIRegions
    {
        get { return uiRegions; }
    }
    [SerializeField]
    private GameObject toolsBar;

    public override void Init()
    {
        base.Init();

        settings = ResourceManager.GetItemsSettings();
        spriteContainer = ResourceManager.GetSpriteContainer("CountryContainer");

        var country = settings.countries[0];

        minCoord = country.min;
        maxCoord = country.max;

        var regions = country.regions;
        var mapRectSize = new Vector2(spriteContainer.width, spriteContainer.height);
        root.localScale = MapCoordinateHelper.GetScaleFactor();

        uiRegions = new List<UIRegionItem>();
        for (int i = 0; i < regions.Count; i++)
        {
            var region = regions[i];
            var sprite = spriteContainer.GetSprite(region.name);
            var uiRegion = Instantiate(template, root);
            uiRegion.Init(region, minCoord, maxCoord);
            uiRegion.baseImage.sprite = sprite;
            uiRegion.BaseRect.sizeDelta = MapCoordinateHelper.ConvertToUI(Vector2.zero,mapRectSize, sprite.rect.size);
            uiRegion.gameObject.SetActive(true);
            uiRegions.Add(uiRegion);
        }
    }

    public override void OpenWindow()
    {
        base.OpenWindow();
        var uIMapCoordinatesWindow = UIMainController.Instance.GetWindow<UIMapCoordinatesWindow>(UIConstants.WINDOW_MAP_COORDINATES);
        uIMapCoordinatesWindow.CloseWindow();
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }


    public void SwitchToolBarState()
    {
        var isActive = toolsBar.activeSelf;
        toolsBar.SetActive(!isActive);
    }

    public void OnSolarSettingsButtonClick()
    {
        var countrySolarSettingsPopup = UIMainController.Instance.GetWindow<UICountrySolarSettingsPopup>(UIConstants.POPUP_COUNTRY_SOLAR_SETTINGS);
        countrySolarSettingsPopup.OpenPopup(x => { });
    }

    public void OnWindSettingsButtonClick()
    {
        var countryWindSettingsPopup = UIMainController.Instance.GetWindow<UICountryWindSettingsPopup>(UIConstants.POPUP_COUNTRY_WIND_SETTINGS);
        countryWindSettingsPopup.OpenPopup(x => { });
    }
}
