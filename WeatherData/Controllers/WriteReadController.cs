using WeatherData.WeatherServices;

namespace WeatherData.Controllers;

public class WriteReadController : ControllerBase
{
    protected override async Task DrawViewAsync()
    {
        MenuView.ShowWriteMenu();
    }

    protected override async Task<bool> HandleInputAsync()
    {
        var key = Console.ReadKey(true).KeyChar;

        switch (key)
        {
            case '1':
                await WeatherReportWriter.WriteReport("WeatherStats.txt");
                return true;
            case '2':
                WeatherReportReader.ReadReport("WeatherStats.txt");
                return true;
            case '9':
                return false;
            default:
                ShowError("Ogiltigt val!");
                return true;
        }
    }
}