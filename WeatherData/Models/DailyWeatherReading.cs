using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherData.WeatherServices
{
    internal class DailyWeatherReading
    {
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public double Temp { get; set; }
        public double Humidity { get; set; }
        public double MoldRisk { get; set; }
    }
}
