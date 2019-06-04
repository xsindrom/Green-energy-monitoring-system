using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class UICityItem : UIItem<CityItem>
    {
        public Text infoText;
        public Image baseImage;

        public override void Init(CityItem origin, Vector2 minCoordinate, Vector2 maxCoordinate)
        {
            base.Init(origin, minCoordinate, maxCoordinate);
            infoText.text = origin.name;
            gameObject.name = origin.name;
        }
    }
}