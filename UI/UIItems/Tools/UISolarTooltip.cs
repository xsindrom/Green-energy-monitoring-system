using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;

public class UISolarTooltip : UIGenericTooltip<SolarItem>{

    [SerializeField]
    protected Text m_AngleToSouth;
    [SerializeField]
    protected Text m_AngleToHorizont;
    [SerializeField]
    protected Text m_Efficiency;
    [SerializeField]
    protected Text m_Power;
    [SerializeField]
    protected Text m_Square;
    [SerializeField]
    protected Text m_Qs;
    [SerializeField]
    protected Text m_Qd;

    public override void Open()
    {
        base.Open();
        m_AngleToSouth.text = m_Origin.angleToSouth.ToString();
        m_AngleToHorizont.text = m_Origin.angleToHorizont.ToString();
        m_Efficiency.text = m_Origin.efficiency.ToString();
        m_Power.text = m_Origin.power.ToString();
        m_Square.text = m_Origin.square.ToString();
        m_Qs.text = m_Origin.Qs.ToString();
        m_Qd.text = m_Origin.Qd.ToString();
    }
}
