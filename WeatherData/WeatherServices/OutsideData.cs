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

        // Detta är metoden (inte en klass)
        public static void GetAvgTempAndHumidityByDate()
        {
            Console.Write("Skriv in datumet (yyyy-MM-dd): ");
            string dateStr = Console.ReadLine();

            // 1. Validering (Krav i uppgiften) [cite: 25]
            if (!DateTime.TryParse(dateStr, out DateTime date))
            {
                Console.WriteLine("Felaktigt format.");
                return;
            }

            // Hämta datan med hjälp av din Helper
            var allData = Helpers.ReadDataForDate(path, date);

            // Filtrera så vi bara ser "Ute" (Krav i uppgiften) [cite: 24, 25]
            var outsideData = allData.Where(r => r.Place.Contains("Ute")).ToList();


            // Räkna ut medel (Krav i uppgiften) [cite: 25, 101]
            if (outsideData.Any())
            {
                double avgT = outsideData.Average(r => r.Temp);
                double avgH = outsideData.Average(r => r.Humidity);
                Console.WriteLine($"Medeltemp: {avgT:F1}, Luftfuktighet: {avgH:F1}");
            }

            Console.ReadLine();
        }

        public static void GetAverageTempByDay()
        {
            Console.Clear();
            Console.WriteLine("Medeltemperatur per dag:\n");
            
            var allData = Helpers.GetWeatherData(path);
            

            var sortedByAvgTemp = allData.Where(r => r.Place.Contains("Ute")).GroupBy(r => r.Date.Date).Select(g => new
                {
                    Date = g.Key,
                    AvgTemp = g.Average(x => x.Temp),
                })
                .OrderByDescending(x => x.AvgTemp).ToList();

            foreach (var day in sortedByAvgTemp)
            {
                Console.WriteLine($"{day.Date:yyyy-MM-dd}: {day.AvgTemp:F2}°C");
            }

            Console.WriteLine("\nTryck valfri knapp för att gå tillbaka...");
            Console.ReadKey();
        }
        
        public static void GetAverageHumidityByDay()
        {
            Console.Clear();
            Console.WriteLine("Torraste till fuktigaste dagen enligt medelfuktighet per dag:\n");
            
            var allData = Helpers.GetWeatherData(path);
            
            var sortedByAvgHumidity = allData.Where(r => r.Place.Contains("Ute")).GroupBy(r => r.Date.Date).Select(g => new
                {
                    Date = g.Key,
                    AvgHumidity = g.Average(x => x.Humidity),
                })
                .OrderBy(x => x.AvgHumidity).ToList();

            foreach (var day in sortedByAvgHumidity)
            {
                Console.WriteLine($"{day.Date:yyyy-MM-dd}: {day.AvgHumidity:F2}%");
            }
            
            Console.WriteLine("\nTryck valfri knapp för att gå tillbaka...");
            Console.ReadKey();
        }
        
        public static void GetMoldRisk()
        {
            var allData = Helpers.GetWeatherData(path);
            Helpers.GetMoldRiskForAllDays(allData);
            
            var sortedByMoldRisk = allData
                .Where(r => r.Place.Contains("Ute"))         
                .GroupBy(r => r.Date.Date)                   
                .Select(g => new
                {
                    Date = g.Key,
                    AvgMoldRisk = g.Average(d => d.MoldRisk) 
                })
                .OrderByDescending(x => x.AvgMoldRisk)      
                .ToList();

            foreach (var day in sortedByMoldRisk)
            {
                Console.WriteLine($"{day.Date:yyyy-MM-dd}: {day.AvgMoldRisk:F2}%");
            }
            
            Console.WriteLine("\nTryck valfri knapp för att gå tillbaka...");
            Console.ReadKey();
        }
    }
}