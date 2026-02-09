using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherData
{
    internal class Helpers
    {
        private static readonly Regex regex =
            new Regex(@"^\s*(?<date>\d{4}-\d{2}-\d{2})\s+\d{2}:\d{2}:\d{2}\s*,\s*(?<place>[^,]+?)\s*,\s*(?<temp>-?\d+(?:[.,]\d+)?)\s*(?:,\s*(?<other>[^,]*))?\s*$");

        public static List<double> ReadTempsForDate(string path, DateTime date)
        {
            var temps =  new List<double>();
            var dateStr = date.ToString("yyyy-MM-dd");
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    int shown = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
                        var match = regex.Match(line);

                        if (!match.Success) // Hoppar över ej rätta matchningar
                        {
                            continue;
                        }

                        if (match.Groups["date"].Value != dateStr)
                        {
                            continue;
                        }
                        
                        if (shown++ < 5) Console.WriteLine("No match: " + line);

                        var tempStr = match.Groups["temp"].Value.Replace(',', '.');
                        temps.Add(double.Parse(tempStr, CultureInfo.InvariantCulture));

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
   
            return temps;
        }
    }
}
