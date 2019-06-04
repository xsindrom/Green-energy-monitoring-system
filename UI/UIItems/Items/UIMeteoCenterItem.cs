using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Items
{
    public class UIMeteoCenterItem : UIItem<MeteoCenterItem>
    {
        public Text infoText;

        public override void Init(MeteoCenterItem origin, Vector2 minCoordinate, Vector2 maxCoordinate)
        {
            base.Init(origin, minCoordinate, maxCoordinate);
            infoText.text = origin.name;
        }
    }
}