using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Network;
using Network.GoogleTables;

public class UICountryWindSettingsPopup : UIBasePopup
{
    [SerializeField]
    private List<Toggle> months = new List<Toggle>();
    [SerializeField]
    private List<float> values = new List<float>();
    [SerializeField]
    private Diagram diagram;
    public List<WindStats> regionStats = new List<WindStats>();

    public override void Init()
    {
        base.Init();

        var uiCountryMapWindow = UIMainController.Instance.GetWindow<UICountryMapWindow>(UIConstants.WINDOW_COUNTRY_MAP);
        var uiRegions = uiCountryMapWindow.UIRegions;
        GoogleTableRequest request = new GoogleTableRequest(NetworkConstants.GOOGLE_TABLE_ID,
                                             "'WindData'!A2:M26",
                                             MajorDimension.COLUMNS);

        Server.Instance.Post<GoogleTableRequest, GoogleTableResponce>(request, x =>
        {
            if (x.status == Status.OK)
            {
                var keys = x.table.columns[0];
                for (int i = 0; i < uiRegions.Count; i++)
                {
                    var region = uiRegions[i];
                    var index = keys.rows.IndexOf(region.Origin.name);

                    var regionStat = new WindStats();
                    if (index >= 0)
                    {
                        for (int j = 1; j < x.table.columns.Count; j++)
                        {
                            var column = x.table.columns[j];
                            var speedRow = column.rows[index];

                            float speed = 0.0f;

                            float.TryParse(speedRow, out speed);

                            var stats = new WindStatsData()
                            {
                                speed = speed
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
                    value += regionStats.speed;
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

            var color = Mathf.CorrelatedColorTemperatureToRGB(value * 1000f);
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
                                                 diagramPart.Origin.percentage * 100f);
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
public class WindStats
{
    public List<WindStatsData> stats = new List<WindStatsData>();
}

[Serializable]
public class WindStatsData
{
    public float speed;
}
