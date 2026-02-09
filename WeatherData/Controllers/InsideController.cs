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
            // metod
            return true;
         case '2':
            // metod
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