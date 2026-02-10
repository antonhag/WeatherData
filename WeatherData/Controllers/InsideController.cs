using WeatherData.WeatherServices;

namespace WeatherData.Controllers;

public class InsideController : ControllerBase
{
   protected override async Task DrawViewAsync()
   {
      MenuView.ShowInsideMenu();
   }

   protected override async Task<bool> HandleInputAsync()
   {
      var key = Console.ReadKey(true).KeyChar;

      switch (key)
      {
         case '1':
            WeatherAnalysis.PrintAvgTempAndHumidityByDate("Inne");
            return true;
         case '2':
            WeatherAnalysis.PrintAverageTempByDay("Inne");
            return true;
         case '3':
            WeatherAnalysis.PrintAverageHumidityByDay("Inne");
            return true;
         case '4':
            WeatherAnalysis.PrintMoldRisk("Inne");
            return true;
         case '9':
            return false;
         default:
            ShowError("Ogiltigt val!");
            return true;
      }
   }
}