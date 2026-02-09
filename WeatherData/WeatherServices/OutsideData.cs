using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData.WeatherServices
{
    internal class OutsideData
    {
        public static string path = "../../../Files/Data.txt";
        
        public static void GetTempByDate()
        {
            Console.Write("Skriv in datumet du vill få info om (yyyy-MM-dd): ");
            string dateStr = Console.ReadLine();
            
            DateTime date = DateTime.ParseExact(dateStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            
            List<double> temps = Helpers.ReadTempsForDate(path, date);

            if (temps.Count == 0)
            {
                Console.WriteLine("No data found");
            }
            else
            {
                foreach (double temp in temps)
                {
                    Console.WriteLine(temp);
                }
            }
            
            Console.ReadKey();
        }
    }
}
