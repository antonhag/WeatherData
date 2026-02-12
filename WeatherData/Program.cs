using WeatherData.Controllers;
using WeatherData.Files;
using WeatherData.WeatherServices;

namespace WeatherData;

class Program
{
    static async Task Main(string[] args)
    {
        await new MenuController().RunAsync();
    }
}