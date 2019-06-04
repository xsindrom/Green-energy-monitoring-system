using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class WindMathController
{
    private const float R = 8.31f;
    private const float M = 0.029f;
    private const float ToPascal = 100f;

    private const float heightMeteo = 3f;
    private const float alpha = 0.2f;

    public static float GetEnergyNow(WindmillItem origin)
    {
        var airDensity = origin.pressure * ToPascal * M / (R * (origin.temperature));
        var square = Mathf.PI * Mathf.Pow(origin.radius, 2);

        var windSpeed = origin.windSpeed * Mathf.Pow(origin.heightWindmill / heightMeteo, alpha);

        var totalEnergy = (origin.efficiency / 100f) * airDensity * Mathf.Pow(windSpeed, 3) * square / 2f;

        return totalEnergy;
    }

}
