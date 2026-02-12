using WeatherData.WeatherServices;

namespace WeatherData.Controllers;

public class WriteController : ControllerBase
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
                await WeatherReportWriter.WriteAvgTempByMonth("WeatherStats.txt");
                return true;
            case '2':
                
                return true;
            case '3':
                
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