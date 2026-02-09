using WeatherData.Controllers;
using WeatherData.Files;

namespace WeatherData;

class Program
{
    static async Task Main(string[] args)
    {
        // while (true)
        // {
        //     Console.Clear();
        //     MenuView.ShowMenu();
        //
        //     ReadWriteFiles.ReadAll("Data.txt");
        //     Console.WriteLine("Välj ut data"); //?
        //     string text = Console.ReadLine();
        //     await ReadWriteFiles.WriteRow("Data.txt", text);
        //     
        // }
        
        await new MenuController().RunAsync();
    }
}