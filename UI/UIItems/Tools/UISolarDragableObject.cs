using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.EventSystems;

public class UISolarDragableObject : UIDragableObject<UISolarItem>
{
    [SerializeField]
    protected SolarItem m_OriginTemplate;
    [SerializeField]
    protected UISolarLayer m_Source;

    public SolarItem OriginTemplate
    {
        get { return m_OriginTemplate; }
        set { m_OriginTemplate = value; }
    }

    public UISolarLayer Source
    {
        get { return m_Source; }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        var mouseCoordinates = MapCoordinateHelper.ConvertToCoordinates(m_Source.MinCoords,
                                                                        m_Source.MaxCoords,
                                                                        eventData.position);
        m_OriginTemplate.Coordinates = mouseCoordinates;
        targetObject = m_Source.Create(SolarItem.Copy(m_OriginTemplate));
        targetObject.Dragable = true;
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        targetObject.OnDrag(eventData);
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        targetObject.OnEndDrag(eventData);
        targetObject.Dragable = false;
        targetObject = null;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        var regionMapWindow = UIMainController.Instance.GetWindow<UIRegionMapWindow>(UIConstants.WINDOW_REGION_MAP);
        var solarTooltip = regionMapWindow.Tooltips.GetTooltip<UISolarTooltip>();
        solarTooltip.rectTransform.anchoredPosition = MapCoordinateHelper.ConvertMousePositionToUI(eventData.position);
        solarTooltip.Open(m_OriginTemplate);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        var regionMapWindow = UIMainController.Instance.GetWindow<UIRegionMapWindow>(UIConstants.WINDOW_REGION_MAP);
        var solarTooltip = regionMapWindow.Tooltips.GetTooltip<UISolarTooltip>();
        solarTooltip.Close();
    }
}
