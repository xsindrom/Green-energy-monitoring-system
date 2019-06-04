using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Items
{
    public abstract class UIItem<T> : MonoBehaviour,
                                      IBeginDragHandler, IDragHandler, IEndDragHandler,
                                      IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
                                      where T: Item
    {
        protected T origin;
        public T Origin
        {
            get { return origin; }
        }

        [SerializeField]
        protected bool dragable;
        public bool Dragable
        {
            get { return dragable; }
            set { dragable = value; }
        }

        [SerializeField]
        protected RectTransform baseRect;
        public RectTransform BaseRect
        {
            get
            {
                return baseRect;
            }
        }

        protected Vector2 minCoordinate;
        protected Vector2 maxCoordinate;

        public UnityEvent onBeginDrag;
        public UnityEvent onDrag;
        public UnityEvent onEndDrag;

        public virtual void Init(T origin, Vector2 minCoordinate, Vector2 maxCoordinate)
        {
            this.origin = origin;
            this.minCoordinate = minCoordinate;
            this.maxCoordinate = maxCoordinate;
            this.baseRect.anchoredPosition = MapCoordinateHelper.ConvertToUI(minCoordinate, maxCoordinate, origin.Coordinates) -
                                                                             MapCoordinateHelper.GetUIOffset();
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            EventHelper.SafeCall(onBeginDrag);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (Dragable)
            {
                Vector3 position = Vector2.zero;
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(baseRect, eventData.position, eventData.pressEventCamera, out position))
                {
                    baseRect.position = position;
                    EventHelper.SafeCall(onDrag);
                }
            }
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            var position = baseRect.anchoredPosition + MapCoordinateHelper.GetUIOffset();
            origin.Coordinates = MapCoordinateHelper.ConvertToCoordinates(minCoordinate, maxCoordinate, position);
            EventHelper.SafeCall(onEndDrag);
        }

        protected virtual void AllowDrag() { }

        protected virtual void DisableDrag() { }

        public virtual void OnPointerEnter(PointerEventData eventData) { }

        public virtual void OnPointerUp(PointerEventData eventData) { }

        public virtual void OnPointerExit(PointerEventData eventData) { }
    }
}