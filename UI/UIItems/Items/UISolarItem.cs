using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Items
{
    public class UISolarItem : UIItem<SolarItem>
    {
        [SerializeField]
        private Text infoText;
        [SerializeField]
        private HoldButton holdButton;

        private UISolarLayer m_Source;
        public UISolarLayer Source
        {
            get { return m_Source; }
            set { m_Source = value; }
        }

        private UIMeteoCenterItem m_ClosestMeteo;
        public UIMeteoCenterItem ClosestMeteo
        {
            get { return m_ClosestMeteo; }
            set
            {
                m_ClosestMeteo = value;
                if (m_ClosestMeteo)
                {
                    infoText.text = m_ClosestMeteo.Origin.name;
                }
            }
        }

        public virtual void Init(SolarItem origin, Vector2 minCoordinate, Vector2 maxCoordinate, UIMeteoCenterItem closestMeteo)
        {
            Init(origin, minCoordinate, maxCoordinate);
            ClosestMeteo = closestMeteo;
        }

        public override void Init(SolarItem origin, Vector2 minCoordinate, Vector2 maxCoordinate)
        {
            base.Init(origin, minCoordinate, maxCoordinate);
            infoText.text = Origin.Coordinates.ToString();
            onEndDrag.AddListener(DisableDrag);
            holdButton.onClick.AddListener(OnClick);
            holdButton.onHold.AddListener(OnHold);
        }

        private void OnHold(float holdTime)
        {
            var window = UIMainController.Instance.GetWindow<UIMovePopup>(UIConstants.POPUP_MOVE);
            window.OpenPopup(callbackType =>
            {
                if (callbackType == PopupCallbackType.Success)
                {
                    AllowDrag();
                }
                else
                {
                    Remove();
                }
            });
        }

        public void OnClick()
        {
            OpenSettings();
        }

        private void OpenSettings()
        {
            var window = UIMainController.Instance.GetWindow<UISolarSettingsPopup>(UIConstants.POPUP_SOLAR_BATTERY_SETTINGS);
            window.OpenPopup(this, null);
        }

        protected override void AllowDrag()
        {
            var window = UIMainController.Instance.GetWindow<UIDragAllowPopup>(UIConstants.POPUP_DRAG_ALLOW);
            window.OpenPopup(callbackType =>
            {
                dragable = callbackType == PopupCallbackType.Success;
            });
        }

        protected virtual void Remove()
        {
            var window = UIMainController.Instance.GetWindow<UIRemovePopup>(UIConstants.POPUP_REMOVE);
            window.OpenPopup(callbackType =>
            {
                if (callbackType == PopupCallbackType.Success)
                {
                    Source.Remove(this);
                }
            });
        }

        protected override void DisableDrag()
        {
            OpenSettings();
            infoText.text = Origin.Coordinates.ToString();
            Dragable = false;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            var regionMapWindow = UIMainController.Instance.GetWindow<UIRegionMapWindow>(UIConstants.WINDOW_REGION_MAP);
            var solarTooltip = regionMapWindow.Tooltips.GetTooltip<UISolarTooltip>();
            solarTooltip.rectTransform.anchoredPosition = MapCoordinateHelper.ConvertMousePositionToUI(eventData.position);
            solarTooltip.Open(Origin);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            var regionMapWindow = UIMainController.Instance.GetWindow<UIRegionMapWindow>(UIConstants.WINDOW_REGION_MAP);
            var solarTooltip = regionMapWindow.Tooltips.GetTooltip<UISolarTooltip>();
            solarTooltip.Close();
        }
    }
}