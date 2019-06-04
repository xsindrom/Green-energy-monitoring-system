using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network.Weather
{
    [Serializable]
    public class WeatherResponce : BaseResponce
    {
        public WeatherCoordData coord;
        public WeatherData weather;
        public string Base;
        public WeatherMainData main;
        public float visibility;
        public WeatherWindData wind;
        public WeatherCloudData clouds;
        public int dt;
        public WeatherSysData sys;
        public int id;
        public string name;
        public int code;
    }

    [Serializable]
    public class WeatherCoordData
    {
        public float lon;
        public float lat;
    }
    [Serializable]
    public class WeatherData
    {
        public int id;
        public string main;
        public string description;
    }
    [Serializable]
    public class WeatherMainData
    {
        public float temp;
        public float temp_min;
        public float temp_max;
        public float huminidity;
        public float pressure;
    }
    [Serializable]
    public class WeatherWindData
    {
        public float speed;
        public float deg;
    }
    [Serializable]
    public class WeatherCloudData
    {
        public float all;
    }
    [Serializable]
    public class WeatherSysData
    {
        public int type;
        public int id;
        public float message;
        public string country;
        public int sunrise;
        public int sunset;
    }
}