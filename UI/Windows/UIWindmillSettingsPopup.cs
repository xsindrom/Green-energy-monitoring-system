using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Items;
using Network;
using Network.Weather;
public class UIWindmillSettingsPopup : UIBasePopup
{

    private const float REFRESH_CD = 30f;

    private UIWindmillItem m_Target;
    public UIWindmillItem Target
    {
        get { return m_Target; }
    }

    [SerializeField]
    private InputField radiusInput;
    [SerializeField]
    private InputField efficiencyInput;
    [SerializeField]
    private InputField heightInput;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text energyText;
    [SerializeField]
    private GameObject windmill;
    [SerializeField]
    private GameObject vanes;

    [SerializeField]
    private Text windSpeed;
    [SerializeField]
    private Text windDirection;
    [SerializeField]
    private Text temperature;
    [SerializeField]
    private Text pressure;

    private bool isTimerOn;

    public void OpenPopup(UIWindmillItem target, UnityAction<PopupCallbackType> callback)
    {
        m_Target = target;
        ApplyStats();
        OpenPopup(callback);
        isTimerOn = true;
        UpdateServer();
        UpdateTimer();
    }

    public override void OnSubmitButtonClick()
    {
        UpdateStats();
        base.OnSubmitButtonClick();
    }

    private void ApplyStats()
    {
        radiusInput.text = m_Target.Origin.radius.ToString();
        heightInput.text = m_Target.Origin.heightWindmill.ToString();
        efficiencyInput.text = m_Target.Origin.efficiency.ToString();
    }

    private void UpdateStats()
    {
        m_Target.Origin.radius = float.Parse(radiusInput.text);
        m_Target.Origin.heightWindmill = float.Parse(heightInput.text);
        m_Target.Origin.efficiency = float.Parse(efficiencyInput.text);
    }

    public override void CloseWindow()
    {
        UpdateStats();
        base.CloseWindow();
        isTimerOn = false;
    }

    public void UpdateTimer()
    {
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (isTimerOn)
        {
            UpdateStats();

            var time = DateTime.Now;
            m_Target.Origin.time = time;
            energyText.text = WindMathController.GetEnergyNow(m_Target.Origin).ToString("F3") + " kVt*h";
            timeText.text = time.ToString("HH:mm:ss");
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void UpdateServer()
    {
        StartCoroutine(UpdateServerCoroutine());
    }

    private IEnumerator UpdateServerCoroutine()
    {
        while (isTimerOn)
        {
            var request = new WeatherRequest(m_Target.Origin.Coordinates);
            Server.Instance.Post<WeatherRequest, WeatherResponce>(request, x =>
            {
                if (x.status == Status.OK)
                {
                    m_Target.Origin.temperature = x.main.temp;
                    m_Target.Origin.windSpeed = x.wind.speed;
                    m_Target.Origin.pressure = x.main.pressure;
                    windDirection.text = x.wind.deg.ToString("F2") + " °";
                    windSpeed.text = x.wind.speed.ToString("F2") + " m/s";
                    temperature.text = (x.main.temp - 273.15f).ToString("F2") + " °С";
                    pressure.text = (x.main.pressure).ToString("F1") + " mBar";
                    windmill.transform.eulerAngles = new Vector3(0, -x.wind.deg, 0);
                    energyText.text = WindMathController.GetEnergyNow(m_Target.Origin).ToString("F2");
                }
            });
            yield return new WaitForSeconds(REFRESH_CD);
        }
    }

    private void FixedUpdate()
    {
        vanes.transform.eulerAngles -= m_Target.Origin.windSpeed * Vector3.forward;
    }

    public void SaveTemplate()
    {
        var regionMapWindow = UIMainController.Instance.GetWindow<UIRegionMapWindow>(UIConstants.WINDOW_REGION_MAP);
        var toolsContainer = regionMapWindow.ToolsPanel.GetToolsContainer<UIWindToolsContainer>();
        toolsContainer.AddTool(WindmillItem.Copy(m_Target.Origin));
    }
}
