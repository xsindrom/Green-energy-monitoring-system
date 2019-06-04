using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;
using Network;
using Network.GoogleTables;

public class UICountrySolarSettingsPopup : UIBasePopup
{
    [SerializeField]
    private List<Toggle> months = new List<Toggle>();

    [SerializeField]
    private Toggle sToggle;
    [SerializeField]
    private Toggle dToggle;
    [SerializeField]
    private List<float> values = new List<float>();
    [SerializeField]
    private Diagram diagram;

    public List<SolarStats> regionStats = new List<SolarStats>();

    public override void Init()
    {
        base.Init();

        var uiCountryMapWindow = UIMainController.Instance.GetWindow<UICountryMapWindow>(UIConstants.WINDOW_COUNTRY_MAP);
        var uiRegions = uiCountryMapWindow.UIRegions;
        GoogleTableRequest request = new GoogleTableRequest(NetworkConstants.GOOGLE_TABLE_ID,
                                                            "'SolarData'!A2:M64",
                                                            MajorDimension.COLUMNS);

        Server.Instance.Post<GoogleTableRequest, GoogleTableResponce>(request, x =>
        {
            if (x.status == Status.OK)
            {
                var keys = x.table.columns[0];

                for (int i = 0; i < uiRegions.Count; i++)
                {
                    var uiRegion = uiRegions[i];
                    var iS = keys.rows.IndexOf(uiRegion.name + ".s");
                    var iD = keys.rows.IndexOf(uiRegion.name + ".d");
                    var iSD = keys.rows.IndexOf(uiRegion.name + ".sd");

                    var regionStat = new SolarStats();
                    if (iS >= 0 && iD >= 0 && iSD >= 0)
                    {
                        for (int j = 1; j < x.table.columns.Count; j++)
                        {
                            var column = x.table.columns[j];
                            var sRow = column.rows[iS];
                            var dRow = column.rows[iD];
                            var sdRow = column.rows[iSD];

                            float s = 0.0f;
                            float d = 0.0f;
                            float sd = 0.0f;

                            float.TryParse(sRow, out s);
                            float.TryParse(dRow, out d);
                            float.TryParse(sdRow, out sd);

                            var stats = new SolarStatsData()
                            {
                                s = s,
                                d = d,
                                sd = sd
                            };
                            regionStat.stats.Add(stats);
                        }
                    }
                    regionStats.Add(regionStat);
                }
            }
        });
    }

    private ColorBlock cachedColorBlock;
    public void SetFilter()
    {
        values.Clear();

        var uiCountryMapWindow = UIMainController.Instance.GetWindow<UICountryMapWindow>(UIConstants.WINDOW_COUNTRY_MAP);
        var regions = uiCountryMapWindow.UIRegions;

        var selectedIndexes = new List<int>();
        for (int i = 0; i < months.Count; i++)
        {
            var month = months[i];
            if (month.isOn)
            {
                selectedIndexes.Add(i);
            }
        }

        for (int i = 0; i < regions.Count; i++)
        {
            var region = regionStats[i];
            var value = 0.0f;
            var count = selectedIndexes.Count;
            for (int j = 0; j < count; j++)
            {
                var index = selectedIndexes[j];
                if (index < region.stats.Count)
                {
                    var regionStats = region.stats[index];
                    value += sToggle.isOn ? regionStats.s : 0.0f;
                    value += dToggle.isOn ? regionStats.d : 0.0f;
                }
            }
            values.Add(value / count);
        }

        diagram.Init(values);
        diagram.Draw();

        for (int i = 0; i < diagram.UIParts.Count; i++)
        {
            var region = regions[i];
            if (cachedColorBlock == default(ColorBlock))
            {
                cachedColorBlock = regions[i].button.colors;
            }
            var value = values[i];
            var diagramPart = diagram.UIParts.Find(x => x.Origin.value == value);

            var color = Mathf.CorrelatedColorTemperatureToRGB(1000f + value * 40f);
            region.button.colors = new ColorBlock()
            {
                normalColor = color,
                pressedColor = cachedColorBlock.pressedColor,
                highlightedColor = cachedColorBlock.highlightedColor,
                disabledColor = cachedColorBlock.disabledColor,
                colorMultiplier = cachedColorBlock.colorMultiplier,
                fadeDuration = cachedColorBlock.fadeDuration
            };

            region.infoText.color = diagramPart.BaseImage.color;

            region.infoText.text = string.Format("{0}\n{1}\n({2}%)",
                                                 region.Origin.name,
                                                 diagramPart.Origin.value,
                                                 diagramPart.Origin.percentage*100f);
        }
    }

    public override void OnDenyButtonClick()
    {
        base.OnDenyButtonClick();
        var uiCountryMapWindow = UIMainController.Instance.GetWindow<UICountryMapWindow>(UIConstants.WINDOW_COUNTRY_MAP);
        var regions = uiCountryMapWindow.UIRegions;
        for (int i = 0; i < regions.Count; i++)
        {
            var region = regions[i];
            region.button.colors = cachedColorBlock;
            region.infoText.text = region.Origin.name;
            region.infoText.color = Color.black;
        }
    }
}

[Serializable]
public class SolarStats
{
    public List<SolarStatsData> stats = new List<SolarStatsData>();
}

[Serializable]
public class SolarStatsData
{
    public float s;
    public float d;
    public float sd;
}
