using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDiagramPopup : UIBasePopup
{
    [SerializeField]
    protected InputField time;
    [SerializeField]
    protected Diagram diagram;

    public override void OpenWindow()
    {
        var regionMap = UIMainController.Instance.GetWindow<UIRegionMapWindow>(UIConstants.WINDOW_REGION_MAP);
        //var solarItems = regionMap.UISolarItems;
    }

}
