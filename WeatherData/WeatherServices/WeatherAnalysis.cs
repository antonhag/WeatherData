using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData.WeatherServices
{
    internal class WeatherAnalysis
    {
        public static string path = "../../../Files/Data.txt";

        // Detta är metoden (inte en klass)
        public static void PrintAvgTempAndHumidityByDate(string place)
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
            var outsideData = allData.Where(r => r.Place.Contains(place)).ToList();


            // Räkna ut medel (Krav i uppgiften) [cite: 25, 101]
            if (outsideData.Any())
            {
                double avgT = outsideData.Average(r => r.Temp);
                double avgH = outsideData.Average(r => r.Humidity);
                
                Console.Clear();
                Console.WriteLine($"Data för dagen ({(place.Contains("Ute") ? "Utomhus" : "Inomhus")}): {date.ToShortDateString()}");
                Console.WriteLine($"\tMedeltemp: {avgT:F1}, Luftfuktighet: {avgH:F1}");
            }

            Console.WriteLine("\nTryck valfri knapp för att gå tillbaka...");
            Console.ReadKey();
        }

        public static void PrintAverageTempByDay(string place)
        {
            Console.Clear();
            Console.WriteLine($"Medeltemperatur per dag ({(place.Contains("Ute") ? "Utomhus" : "Inomhus")}):\n");
            
            var allData = Helpers.GetWeatherData(path);
            

            var sortedByAvgTemp = allData.Where(r => r.Place.Contains(place)).GroupBy(r => r.Date.Date).Select(g => new
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
        
        public static void PrintAverageHumidityByDay(string place)
        {
            Console.Clear();
            Console.WriteLine($"Torraste till fuktigaste dagen enligt medelfuktighet per dag ({(place.Contains("Ute") ? "Utomhus" : "Inomhus")}):\n");
            
            var allData = Helpers.GetWeatherData(path);
            
            var sortedByAvgHumidity = allData.Where(r => r.Place.Contains(place)).GroupBy(r => r.Date.Date).Select(g => new
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
        
        public static void PrintMoldRisk(string place)
        {
            Console.Clear();
            Console.WriteLine($"Sortering av dagar från minst till störst risk för mögel ({(place.Contains("Ute") ? "Utomhus" : "Inomhus")}):\n");
            
            var allData = Helpers.GetWeatherData(path);
            Helpers.GetMoldRiskForAllDays(allData);
            
            var sortedByMoldRisk = allData
                .Where(r => r.Place.Contains(place))         
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