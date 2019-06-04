using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace UnityEngine.UI
{
    public class HoldButton : Button, IUpdateSelectedHandler
    {
        protected const float CLICK_TIME = 0.25f;

        public HoldEvent onHold;

        protected bool m_Pressed;
        public bool Pressed
        {
            get
            {
                return m_Pressed;
            }
        }

        protected float m_HoldTime;
        public float HoldTime
        {
            get
            {
                return m_HoldTime;
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            m_Pressed = true;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            m_Pressed = false;
          
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (m_HoldTime < CLICK_TIME)
            {
                base.OnPointerClick(eventData);
            }
            else
            {
                if (onHold != null)
                {
                    onHold.Invoke(m_HoldTime);
                }
            }
            m_HoldTime = 0.0f;
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            if (m_Pressed)
            {
                m_HoldTime += Time.deltaTime;
            }
        }
    }

    [System.Serializable]
    public class HoldEvent: UnityEvent<float>
    {

    }
}