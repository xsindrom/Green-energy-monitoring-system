using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class SolarMathController {

    private const int S_MAX = 1362;
    private const int U_Test = 1000;

    private const float MEREDIAN_WIDTH = 15f;
    private const float MINUTES_PER_DEGREE = 4f;

    public static float GetEnergyNow(SolarItem origin)
    {
        var dayNumber = origin.time.DayOfYear;
        var time = (float)origin.time.TimeOfDay.TotalHours;

        var utc = Mathf.RoundToInt((float)(DateTime.Now - DateTime.UtcNow).TotalHours);

        var solarTime = Mathf.Abs((origin.Coordinates.x - utc * MEREDIAN_WIDTH) * MINUTES_PER_DEGREE / 60 + time);

        var solarIncline = 23.45f * Mathf.Sin(2 * Mathf.PI * (284 + dayNumber) / 365);

        var tempAngle = Mathf.PI * (12 - solarTime) / 12;

        var sinSolarHeight = Mathf.Cos(tempAngle) * 
                             Mathf.Cos(solarIncline * Mathf.Deg2Rad) *
                             Mathf.Cos(origin.Coordinates.y * Mathf.Deg2Rad) +
                             Mathf.Sin(solarIncline * Mathf.Deg2Rad) *
                             Mathf.Sin(origin.Coordinates.y * Mathf.Deg2Rad);

        var cosSolarAngle = sinSolarHeight *
                             Mathf.Cos(origin.angleToHorizont * Mathf.Deg2Rad) +
                             Mathf.Sin(origin.angleToHorizont * Mathf.Deg2Rad) *
                             (Mathf.Cos(origin.angleToSouth * Mathf.Deg2Rad) *
                             Mathf.Tan(origin.Coordinates.y * Mathf.Deg2Rad) *
                             sinSolarHeight +
                             Mathf.Sin(origin.angleToSouth * Mathf.Deg2Rad) *
                             Mathf.Cos(solarIncline * Mathf.Deg2Rad) *
                             Mathf.Sin(tempAngle));

        var atmoFactor = 1.1254f - 0.1366f / sinSolarHeight;

        var directEnergyFlow = S_MAX * cosSolarAngle * atmoFactor; //D
        var scatteredEnergyFlow = 137.1f - 14.82f / sinSolarHeight; //S

        var flowSum = directEnergyFlow * origin.Qs + scatteredEnergyFlow * origin.Qd;
        var totalEnergy = origin.power * (origin.efficiency/100f) * origin.square * flowSum * origin.cloudiness / U_Test; // за 1 годину

        return totalEnergy < 0 ? 0 : sinSolarHeight < 0 ? 0 : totalEnergy;
    }

    public static void GetEnergyMaxNow(float yMinValue, float yMaxValue,
                                        float zMinValue, float zMaxValue,
                                        SolarItem origin,
                                        out float maxEnergy,
                                        out float maxAngleToSouth,
                                        out float maxAngleToHorisont)
    {
        maxEnergy = 0f;
        maxAngleToHorisont = 0f;
        maxAngleToSouth = 0f;

        for (int i = (int)yMinValue; i < yMaxValue; i++)
        {
            origin.angleToHorizont = i;
            for (int j = (int)zMinValue; j < zMaxValue; j++)
            {
                origin.angleToSouth = j;
                var valueEnergy = GetEnergyNow(origin);
                if (maxEnergy < valueEnergy)
                {
                    maxEnergy = valueEnergy;
                    maxAngleToHorisont = i;
                    maxAngleToSouth = j;
                }
            }
        }
    }

}
