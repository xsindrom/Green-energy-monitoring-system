using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITooltipContainer : MonoBehaviour {

    [SerializeField]
    protected List<UITooltip> m_Tooltips = new List<UITooltip>();

    public T GetTooltip<T>() where T: UITooltip
    {
        var tooltip = m_Tooltips.Find(x => x is T);
        if (tooltip)
        {
            return tooltip as T;
        }
        return null;
    }
}
