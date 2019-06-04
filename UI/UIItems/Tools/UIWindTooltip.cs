using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;

public class UIWindTooltip : UIGenericTooltip<WindmillItem>
{
    [SerializeField]
    protected Text m_Efficiency;
    [SerializeField]
    protected Text m_Radius;
    [SerializeField]
    protected Text m_HeightWindmill;

    public override void Open()
    {
        base.Open();
        m_Efficiency.text = m_Origin.efficiency.ToString();
        m_Radius.text = m_Origin.radius.ToString();
        m_HeightWindmill.text = m_Origin.heightWindmill.ToString();
    }
}
