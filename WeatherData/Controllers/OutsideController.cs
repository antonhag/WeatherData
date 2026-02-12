using WeatherData.WeatherServices;

namespace WeatherData.Controllers;

public class OutsideController : ControllerBase
{
    protected override async Task DrawViewAsync()
    {
        MenuView.ShowOutsideMenu();
    }

    protected override async Task<bool> HandleInputAsync()
    {
        var key = Console.ReadKey(true).KeyChar;

        switch (key)
        {
            case '1':
                WeatherAnalysis.PrintAvgTempAndHumidityByDate("Ute");
                return true;
            case '2':
                WeatherAnalysis.PrintAverageTempByDay("Ute");
                return true;
            case '3':
                WeatherAnalysis.PrintAverageHumidityByDay("Ute");
                return true;
            case '4':
                WeatherAnalysis.PrintMoldRisk("Ute");
                return true;
            case '5':
                Console.WriteLine(WeatherAnalysis.GetMeteorologicalSeason(10.0));
                Console.WriteLine("\nTryck valfri knapp för att gå tillbaka...");
                Console.ReadKey();
                return true;
            case '6':
                Console.WriteLine(WeatherAnalysis.GetMeteorologicalSeason(0.0));
                Console.WriteLine("\nTryck valfri knapp för att gå tillbaka...");
                Console.ReadKey();
                return true;
            case '9':
                return false;
            default:
                ShowError("Ogiltigt val!");
                return true;
        }
    }
}