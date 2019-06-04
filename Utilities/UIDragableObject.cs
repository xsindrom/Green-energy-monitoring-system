using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragableObject<T> : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler,
                                   IPointerEnterHandler,IPointerUpHandler, IPointerExitHandler
                                   where T:MonoBehaviour
{
    public T targetObject;

    public UnityPointerDataEvent onDrag;
    public UnityPointerDataEvent onBeginDrag;
    public UnityPointerDataEvent onEndDrag;
    public UnityPointerDataEvent onPointerEnter;
    public UnityPointerDataEvent onPointerUp;
    public UnityPointerDataEvent onPointerExit;

    public virtual void Awake()
    {
        if (onDrag == null)
        {
            onDrag = new UnityPointerDataEvent();
        } 
        if (onBeginDrag == null)
        {
            onBeginDrag = new UnityPointerDataEvent();
        }
        if (onEndDrag == null)
        {
            onEndDrag = new UnityPointerDataEvent();
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        EventHelper.SafeCall(onBeginDrag, eventData);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        EventHelper.SafeCall(onDrag, eventData);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        EventHelper.SafeCall(onEndDrag, eventData);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        EventHelper.SafeCall(onPointerEnter, eventData);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        EventHelper.SafeCall(onPointerExit, eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        EventHelper.SafeCall(onPointerUp, eventData);
    }
}
[Serializable]
public class UnityPointerDataEvent : UnityEvent<PointerEventData>
{

}