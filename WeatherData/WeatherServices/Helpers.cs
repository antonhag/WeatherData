using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherData.WeatherServices;

namespace WeatherData
{
    internal class Helpers
    {
        private static readonly Regex regex = new Regex(
            @"\s*(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{1,2}:\d{2}:\d{2}),(?<place>[A-ZÅÄÖ][a-zåäö]*),(?<temp>-?\d+(\.\d+)?),(?<humidity>\d+)"
        );


        public static List<DailyWeatherReading> GetWeatherDataFromDate(string path, DateTime inputDate)
        {
            var results = new List<DailyWeatherReading>();
            var dateStr = inputDate.ToString("yyyy-MM-dd");

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var match = regex.Match(line);

                        if (!match.Success || match.Groups["date"].Value != dateStr)
                        {
                            continue;
                        }

                        // Tolkar datumsträngen exakt enligt formatet "yyyy-MM-dd" med InvariantCulture,
                        // så att resultatet blir samma oavsett datorns språk-/regionsinställningar 
                        var date = DateTime.ParseExact(match.Groups["date"].Value, "yyyy-MM-dd",
                            CultureInfo.InvariantCulture);
                        var temp = double.Parse(match.Groups["temp"].Value, CultureInfo.InvariantCulture); 
                        var humidity = double.Parse(match.Groups["humidity"].Value, CultureInfo.InvariantCulture);
                        var place = match.Groups["place"].Value.Trim();

                        if ((date.Year == 2016 && date.Month == 5 || date.Year == 2017 && date.Month == 1)) // Hoppar över månader 5 och 12 som ej skulle vara med enligt uppgiften
                        {
                            continue;
                        }

                        // Skapa objektet och lägg till i listan
                        results.Add(new DailyWeatherReading
                        {
                            Date = date,
                            Place = place,
                            Temp = temp,
                            Humidity = humidity
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return results;
        }

        public static List<DailyWeatherReading> GetWeatherData(string path)
        {
            var results = new List<DailyWeatherReading>();
            
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        line = line.Trim();
                        var match = regex.Match(line);

                        if (!match.Success)
                        {
                            continue;
                        }

                        // Tolkar datumsträngen exakt enligt formatet "yyyy-MM-dd" med InvariantCulture,
                        // så att resultatet blir samma oavsett datorns språk-/regionsinställningar
                        var date = DateTime.ParseExact(match.Groups["date"].Value, "yyyy-MM-dd",
                            CultureInfo.InvariantCulture);
                        var temp = double.Parse(match.Groups["temp"].Value, CultureInfo.InvariantCulture);
                        var humidity = double.Parse(match.Groups["humidity"].Value, CultureInfo.InvariantCulture);
                        var place = match.Groups["place"].Value.Trim();

                        if ((date.Year == 2016 && date.Month == 5 || date.Year == 2017 && date.Month == 1))
                        {
                            continue;
                        }

                        results.Add(new DailyWeatherReading
                        {
                            Date = date,
                            Place = place,
                            Temp = temp,
                            Humidity = humidity
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ogiltig rad skipped: {line} -> {ex.Message}");
                        continue; // går vidare till nästa rad
                    }
                }
            }

            return results;
        }


        public static void GetMoldRiskForAllDays(List<DailyWeatherReading> weatherData)
        {
            foreach (var day in weatherData)
            {
                day.MoldRisk = CalculateMoldRisk(day.Temp, day.Humidity);
            }
        }

        private static double CalculateMoldRisk(double temp, double humidity)
        {
            // krav för mögel
            if (temp <= 0 || temp >= 50 || humidity < 70)
            {
                return 0;
            }

            double humidityRisk = ((humidity - 70) / (100 - 70)) * 100;

            // Risk baserat på temperatur
            double tempRisk;
            if (temp >= 20 && temp <= 30)
            {
                tempRisk = 100; // optimal temperatur = full risk
            }
            else if (temp < 20)
            {
                tempRisk = Math.Max(0, (temp / 20) * 100); // minskar linjärt mot 0°C
            }
            else
            {
                tempRisk = Math.Max(0, ((50 - temp) / (50 - 30)) * 100); // minskar linjärt mot 50°C
            }

            // Kombinera risker med vikt: fukt 70%, temp 30%
            double moldRisk = humidityRisk * 0.7 + tempRisk * 0.3;

            // Avrunda till 1 decimal
            return Math.Round(moldRisk, 1);
        }

        public static string MoldRiskFormula()
        {
            string moldRisk = "-------Mögelrisk algoritmen-------";

            moldRisk += "\n\nKrav för mögel: 0°C < T < 50°C och H ≥ 70% (annars 0% risk)";

            moldRisk += "\n\nMögelrisk (%) = 0,7 × ((H - 70) / 30 × 100) + 0,3 × Temperaturrisk";

            moldRisk += "\n\nTemperaturrisk\n\n100% om 20°C ≤ T ≤ 30°C\n(T / 20 × 100) om T < 20\n((50 - T) / 20 × 100) om T > 30";

            return moldRisk;
        }

    }
}