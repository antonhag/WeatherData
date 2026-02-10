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
                OutsideData.GetAvgTempAndHumidityByDate();
                return true;
            case '2':
                OutsideData.GetAverageTempByDay();
                return true;
            case '3':
                OutsideData.GetAverageHumidityByDay();
                return true;
            case '4':
                OutsideData.GetMoldRisk();
                return true;
            case '9':
                return false;
            default:
                ShowError("Ogiltigt val!");
                return true;
        }
    }
}