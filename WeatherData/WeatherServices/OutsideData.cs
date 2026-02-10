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
        public static void GetTempByDate()
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
            var outsideData = allData.Where(r => r.Place.Trim() == "Ute").ToList();


            // Räkna ut medel (Krav i uppgiften) [cite: 25, 101]
            if (outsideData.Any())
            {
                double avgT = outsideData.Average(r => r.Temp);
                double avgH = outsideData.Average(r => r.Humidity);
                Console.WriteLine($"Medeltemp: {avgT:F1}, Luftfuktighet: {avgH:F1}");
            }

            Console.ReadLine();
        }

        public static void AverageTempByDay()
        {
            // Console.Clear();
            
            var allData = Helpers.AverageTempAndHumidityByDay(path);
            

            var sortedByAvgTemp = allData.Where(r => r.Place.Contains("Ute")).GroupBy(r => r.Date.Date).Select(g => new
                {
                    Date = g.Key,
                    AvgTemp = g.Average(x => x.Temp),
                })
                .OrderBy(x => x.Date).ToList();

            foreach (var day in sortedByAvgTemp)
            {
                Console.WriteLine($"{day.Date:yyyy-MM-dd}: {day.AvgTemp:F2}°C");
            }

            Console.WriteLine("Tryck valfri knapp för att gå tillbaka...");
            Console.ReadKey();
        }
    }
}