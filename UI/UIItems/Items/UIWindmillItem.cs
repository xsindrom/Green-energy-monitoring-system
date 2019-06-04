using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Items
{
    public class UIWindmillItem : UIItem<WindmillItem>
    {
        [SerializeField]
        private Text infoText;
        [SerializeField]
        private HoldButton holdButton;

        private UIWindLayer m_Source;
        public UIWindLayer Source
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

        public virtual void Init(WindmillItem origin, Vector2 minCoordinate, Vector2 maxCoordinate, UIMeteoCenterItem closestMeteo)
        {
            Init(origin, minCoordinate, maxCoordinate);
            ClosestMeteo = closestMeteo;
        }

        public override void Init(WindmillItem origin, Vector2 minCoordinate, Vector2 maxCoordinate)
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
            var window = UIMainController.Instance.GetWindow<UIWindmillSettingsPopup>(UIConstants.POPUP_WINDMILL_SETTINGS);
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
            base.OnPointerEnter(eventData);
            var regionMap = UIMainController.Instance.GetWindow<UIRegionMapWindow>(UIConstants.WINDOW_REGION_MAP);
            var windTooltip = regionMap.Tooltips.GetTooltip<UIWindTooltip>();
            windTooltip.rectTransform.anchoredPosition = MapCoordinateHelper.ConvertMousePositionToUI(eventData.position);
            windTooltip.Open(Origin);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            var regionMap = UIMainController.Instance.GetWindow<UIRegionMapWindow>(UIConstants.WINDOW_REGION_MAP);
            var windTooltip = regionMap.Tooltips.GetTooltip<UIWindTooltip>();
            windTooltip.Close();
        }
    }
}