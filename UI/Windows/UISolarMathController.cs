using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;


public class UISolarMathController : MonoBehaviour
{
    public const float UPDATE_RATE = 5.0f;
    public const float MIN_ANGLE_TO_SOUTH = -180f;
    public const float MAX_ANGLE_TO_SOUTH = 180f;
    public const float MIN_ANGLE_TO_HORISONT = 0f;
    public const float MAX_ANGLE_TO_HORISONT = 90f;
    public const string COORDINATES_COLUMN_NAME = "Coordinates";
    public const string ENERGY_COLUMN_NAME = "Energy";
    public const string AREA_COLUMN_NAME = "Area";
    public const string ClOUDS_COLUMN_NAME = "Clouds";
    public const string POWER_COLUMN_NAME = "Power";

    public bool m_IsRunning = true;

    [SerializeField]
    protected UISolarLayer m_Layer;
    [SerializeField]
    protected UITable m_Table;

    private void OnEnable()
    {
        m_Table.Clear();
        StartCoroutine(UpdateRoutine());
    }

    private IEnumerator UpdateRoutine()
    {
        while (m_IsRunning)
        {
            var items = m_Layer.UIItems;
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i].Origin;
                item.time = DateTime.Now;

                var maxEnergy = 0.0f;
                var maxAngleToSouth = 0.0f;
                var maxAngleToHorisont = 0.0f;

                SolarMathController.GetEnergyMaxNow(MIN_ANGLE_TO_HORISONT, MAX_ANGLE_TO_HORISONT,
                                                    MIN_ANGLE_TO_SOUTH, MAX_ANGLE_TO_SOUTH,
                                                    item,
                                                    out maxEnergy,
                                                    out maxAngleToSouth,
                                                    out maxAngleToHorisont);

                var energy = maxEnergy;

                var row = m_Table.CreateRow();
                row[COORDINATES_COLUMN_NAME].text = item.Coordinates.ToString();
                row[ENERGY_COLUMN_NAME].text = energy.ToString();
                row[AREA_COLUMN_NAME].text = item.square.ToString();
                row[ClOUDS_COLUMN_NAME].text = (1 - item.cloudiness).ToString();
                row[POWER_COLUMN_NAME].text = item.power.ToString();
            }
            yield return new WaitForSeconds(UPDATE_RATE);
            m_Table.Clear();
        }
    }

    public void OnExportButtonClick()
    {
        File.WriteAllText(Application.persistentDataPath + '/' + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".csv", m_Table.ToString());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
