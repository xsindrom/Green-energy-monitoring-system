using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.EventSystems;

public class UIWindmillDragableObject : UIDragableObject<UIWindmillItem>
{
    [SerializeField]
    protected WindmillItem m_OriginTemplate;
    [SerializeField]
    protected UIWindLayer m_Source;

    public WindmillItem OriginTemplate
    {
        get { return m_OriginTemplate; }
        set { m_OriginTemplate = value; }
    }

    public UIWindLayer Source
    {
        get { return m_Source; }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        var mouseCoordinates = MapCoordinateHelper.ConvertToCoordinates(m_Source.MinCoords,
                                                                        m_Source.MaxCoords,
                                                                        eventData.position);
        m_OriginTemplate.Coordinates = mouseCoordinates;
        targetObject = m_Source.Create(WindmillItem.Copy(m_OriginTemplate));
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
        base.OnPointerEnter(eventData);
        var regionMap = UIMainController.Instance.GetWindow<UIRegionMapWindow>(UIConstants.WINDOW_REGION_MAP);
        var windTooltip = regionMap.Tooltips.GetTooltip<UIWindTooltip>();
        windTooltip.rectTransform.anchoredPosition = MapCoordinateHelper.ConvertMousePositionToUI(eventData.position);
        windTooltip.Open(m_OriginTemplate);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        var regionMap = UIMainController.Instance.GetWindow<UIRegionMapWindow>(UIConstants.WINDOW_REGION_MAP);
        var windTooltip = regionMap.Tooltips.GetTooltip<UIWindTooltip>();
        windTooltip.Close();
    }
}
