namespace WeatherData.Controllers;

public class MenuController : ControllerBase
{
    protected override async Task DrawViewAsync()
    {
        MenuView.ShowMenu();
    }

    protected override async Task<bool> HandleInputAsync()
    {
        var key = Console.ReadKey(true).KeyChar;

        switch (key)
        {
            case '1':
                await new OutsideController().RunAsync();
                return true;
            case '2':
                await new InsideController().RunAsync();
                return true;
            case '3':
                await new WriteReadController().RunAsync();
                return true;
            default:
                ShowError("Ogiltigt val!");
                return true;
        }
    }
}