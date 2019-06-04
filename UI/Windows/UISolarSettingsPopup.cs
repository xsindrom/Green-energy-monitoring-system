using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Items;
using Network;
using Network.Weather;

public class UISolarSettingsPopup : UIBasePopup
{
    private const float REFRESH_CD = 30f;

    private UISolarItem m_Target;
    public UISolarItem Target
    {
        get { return m_Target; }
    }

    [SerializeField]
    private Transform battery;
    [SerializeField]
    private Slider yRotationSlider;
    [SerializeField]
    private Slider zRotationSlider;
    [SerializeField]
    private Text yRotationText;
    [SerializeField]
    private Text zRotationText;

    [SerializeField]
    private InputField powerInput;
    [SerializeField]
    private InputField efficiencyInput;
    [SerializeField]
    private InputField squareInput;
    [SerializeField]
    private InputField QsInput;
    [SerializeField]
    private InputField QdInput;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text energyText;
    [SerializeField]
    private Text cloudinessText;


    private bool isTimerOn;

    public void OpenPopup(UISolarItem target, UnityAction<PopupCallbackType> callback)
    {
        m_Target = target;

        yRotationSlider.onValueChanged.AddListener((angle) =>
        {
            var rotation = battery.eulerAngles;
            rotation.y = angle;
            battery.eulerAngles = rotation;
            yRotationText.text = angle.ToString();
        });
        zRotationSlider.onValueChanged.AddListener((angle) =>
        {
            var rotation = battery.eulerAngles;
            rotation.z = angle;
            battery.eulerAngles = rotation;
            zRotationText.text = angle.ToString();
        });

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
        zRotationSlider.value = m_Target.Origin.angleToHorizont;
        yRotationSlider.value = m_Target.Origin.angleToSouth;
        efficiencyInput.text = m_Target.Origin.efficiency.ToString();
        powerInput.text = m_Target.Origin.power.ToString();
        squareInput.text = m_Target.Origin.square.ToString();
        QsInput.text = m_Target.Origin.Qs.ToString();
        QdInput.text = m_Target.Origin.Qd.ToString();
        cloudinessText.text = m_Target.Origin.cloudiness.ToString();
    }

    private void UpdateStats()
    {
        m_Target.Origin.angleToHorizont = float.Parse(zRotationText.text);
        m_Target.Origin.angleToSouth = float.Parse(yRotationText.text);
        m_Target.Origin.efficiency = float.Parse(efficiencyInput.text);
        m_Target.Origin.power = float.Parse(powerInput.text);
        m_Target.Origin.square = float.Parse(squareInput.text);
        m_Target.Origin.Qs = float.Parse(QsInput.text);
        m_Target.Origin.Qd = float.Parse(QdInput.text);
    }

    public override void CloseWindow()
    {
        base.CloseWindow();
        yRotationSlider.onValueChanged.RemoveAllListeners();
        zRotationSlider.onValueChanged.RemoveAllListeners();
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
            energyText.text = SolarMathController.GetEnergyNow(m_Target.Origin).ToString("F2");
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
                    m_Target.Origin.cloudiness = 1 - x.clouds.all / 100f;
                    cloudinessText.text = x.clouds.all.ToString();
                }
            });
            yield return new WaitForSeconds(REFRESH_CD);
        }
    }

    public void GetMaxUsefulAngles()
    {
        var maxEnergy = 0f;
        var maxAngleToSouth = 0f;
        var maxAngleToHorisont = 0f;

        SolarMathController.GetEnergyMaxNow((int)yRotationSlider.minValue,
                                            (int)yRotationSlider.maxValue,
                                            (int)zRotationSlider.minValue,
                                            (int)zRotationSlider.maxValue,
                                            m_Target.Origin,
                                            out maxEnergy,
                                            out maxAngleToSouth,
                                            out maxAngleToHorisont);
        zRotationSlider.value = maxAngleToHorisont;
        yRotationSlider.value = maxAngleToSouth;
        energyText.text = maxEnergy.ToString();
    }

    public void SaveTemplate()
    {
        var regionMapWindow = UIMainController.Instance.GetWindow<UIRegionMapWindow>(UIConstants.WINDOW_REGION_MAP);
        var toolsContainer = regionMapWindow.ToolsPanel.GetToolsContainer<UISolarToolsContainer>();
        toolsContainer.AddTool(SolarItem.Copy(m_Target.Origin));
    }
}
