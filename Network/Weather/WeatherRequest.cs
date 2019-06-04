using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network.Weather
{
    public class WeatherRequest : BaseRequest
    {
        private Vector2 coord;
        public Vector2 Coord
        {
            get { return coord; }
        }

        public WeatherRequest(Vector2 coord)
        {
            this.coord = coord;
            path = string.Format(NetworkConstants.OPEN_WEATHER_REQUEST_FORMAT, coord.y, coord.x,NetworkConstants.OPEN_WEATHER_APPID);
        }

        public WeatherRequest(Vector2 coord, string appId)
        {
            this.coord = coord;
            path = string.Format(NetworkConstants.OPEN_WEATHER_REQUEST_FORMAT, coord.y, coord.x, appId);
        }
    }
}