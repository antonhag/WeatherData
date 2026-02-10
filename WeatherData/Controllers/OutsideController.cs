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
                OutsideData.GetTempByDate();
                return true;
            case '2':
                OutsideData.AverageTempByDay();
                return true;
            case '3':
                // metod
                return true;
            case '4':
                return true;
            case '9':
                return false;
            default:
                ShowError("Ogiltigt val!");
                return true;
        }
    }
}