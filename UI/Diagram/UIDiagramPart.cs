using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIDiagramPart : MonoBehaviour
{
    [SerializeField]
    protected DiagramPart origin = new DiagramPart();
    [SerializeField]
    protected Image baseImage;
    [SerializeField]
    protected Text percentageText;
    [SerializeField]
    protected Text valueText;

    public Image BaseImage
    {
        get { return baseImage; }
    }

    public Text PercentageText
    {
        get { return percentageText; }
    }

    public Text ValueText
    {
        get { return valueText; }
    }

    public DiagramPart Origin
    {
        get { return origin; }
        set { origin = value; }
    }

    public virtual void Init(float fillValue, DiagramPart origin)
    {
        this.origin = origin;
        baseImage.fillAmount = fillValue;
        baseImage.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        if (valueText)
        {
            valueText.text = origin.value.ToString();
        }
        if (percentageText)
        {
            percentageText.text = string.Format("{0} %", origin.percentage);
        }
        transform.SetAsFirstSibling();
    }
}

[Serializable]
public class DiagramPart
{
    public float value;
    public float percentage;
}

public class DiagramPartComparer : IComparer<DiagramPart>
{
    public int Compare(DiagramPart x, DiagramPart y)
    {
        return x.value >= y.value ? 1 : -1;
    }
}