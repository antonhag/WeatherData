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
        

        public static List<WeatherReading> ReadDataForDate(string path, DateTime inputDate)
        {
            var results = new List<WeatherReading>();
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
                        
                        var date = DateTime.ParseExact(match.Groups["date"].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        var temp = double.Parse(match.Groups["temp"].Value, CultureInfo.InvariantCulture);
                        var humidity = double.Parse(match.Groups["humidity"].Value, CultureInfo.InvariantCulture);
                        var place = match.Groups["place"].Value.Trim();
                        
                        if ((date.Year == 2016 && date.Month == 5 || date.Year == 2017 && date.Month == 1))
                        {
                            continue;
                        }

                        // 3. Skapa objektet och lägg till i listan
                        results.Add(new WeatherReading
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
        
        public static List<WeatherReading> AverageTempAndHumidityByDay(string path)
        {
            var results = new List<WeatherReading>();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        var match = regex.Match(line);
                        
                        if (!match.Success)
                        {
                            continue;
                        }
                        

                        var date = DateTime.ParseExact(match.Groups["date"].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        var temp = double.Parse(match.Groups["temp"].Value, CultureInfo.InvariantCulture);
                        var humidity = double.Parse(match.Groups["humidity"].Value, CultureInfo.InvariantCulture);
                        var place = match.Groups["place"].Value.Trim();

                        if ((date.Year == 2016 && date.Month == 5 || date.Year == 2017 && date.Month == 1))
                        {
                            continue;
                        }
                        
                        results.Add(new WeatherReading
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
        
        
    }
}
