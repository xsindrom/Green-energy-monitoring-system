using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Items
{
    public class UIRegionItem : UIItem<RegionItem>
    {
        public Image baseImage;
        public Button button;
        public Text infoText;

        public override void Init(RegionItem origin, Vector2 minCoordinate, Vector2 maxCoordinate)
        {
            base.Init(origin, minCoordinate, maxCoordinate);
            infoText.text = origin.name;
            gameObject.name = origin.name;

            baseRect.anchoredPosition = MapCoordinateHelper.ConvertToUI(minCoordinate, maxCoordinate, origin.Coordinates) -
                                                                        MapCoordinateHelper.GetUIOffset();
        }

        public void Action()
        {
            var countryMapWindow = UIMainController.Instance.GetWindow<UICountryMapWindow>(UIConstants.WINDOW_COUNTRY_MAP);
            countryMapWindow.CloseWindow();

            var regionMapWindow = UIMainController.Instance.GetWindow<UIRegionMapWindow>(UIConstants.WINDOW_REGION_MAP);
            regionMapWindow.OpenWindow(this);
        }
    }

   
}