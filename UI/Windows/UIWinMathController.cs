using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWinMathController : MonoBehaviour {
    public const float UPDATE_RATE = 5.0f;
    public const string COORDINATES_COLUMN_NAME = "Coordinates";
    public const string ENERGY_COLUMN_NAME = "Energy";
    public const string RADIUS_COLUMN_NAME = "Radius";
    public const string HEIGHT_COLUMN_NAME = "Height";
    public const string SPEED_COLUMN_NAME = "Speed";
    

    public bool m_IsRunning = true;

    [SerializeField]
    protected UIWindLayer m_Layer;
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

                var energy = WindMathController.GetEnergyNow(item);

                var row = m_Table.CreateRow();
                row[COORDINATES_COLUMN_NAME].text = item.Coordinates.ToString();
                row[ENERGY_COLUMN_NAME].text = energy.ToString();
                row[HEIGHT_COLUMN_NAME].text = item.heightWindmill.ToString();
                row[RADIUS_COLUMN_NAME].text = item.radius.ToString();
                row[SPEED_COLUMN_NAME].text = item.windSpeed.ToString();
                
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
