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
        private static readonly Regex regex =
            new Regex(@"^\s*(?<date>\d{4}-\d{2}-\d{2})\s+\d{2}:\d{2}:\d{2}\s*,\s*(?<place>[^,]+?)\s*,\s*(?<temp>-?\d+(?:[.,]\d+)?)\s*,\s*(?<humidity>\d+)\s*$");

        public static List<WeatherReading> ReadDataForDate(string path, DateTime date)
        {
            var results = new List<WeatherReading>();
            var dateStr = date.ToString("yyyy-MM-dd");

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

                        var tempStr = match.Groups["temp"].Value.Replace('.', ',');
                        var humStr = match.Groups["humidity"].Value;

                        // 3. Skapa objektet och lägg till i listan
                        results.Add(new WeatherReading
                        {
                            Date = date,
                            Place = match.Groups["place"].Value,
                            Temp = double.Parse(tempStr),
                            Humidity = double.Parse(humStr)
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
