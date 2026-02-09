using WeatherData.Files;

namespace WeatherData;

class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Menu.ShowMenu();

            ReadWriteFiles.ReadAll("Data.txt");
            Console.WriteLine("Välj ut data"); //?
            string text = Console.ReadLine();
            await ReadWriteFiles.WriteRow("Data.txt", text);


            var key =  Console.ReadKey(true).KeyChar;

            switch (key)
            {
                case '1':
                    Console.Clear();
                    Console.WriteLine("Tryck på siffran för det du vill ha info om\n");
                    Menu.ShowOutsideMenu();
                   
                    
                    var key2 = Console.ReadKey(true).KeyChar;

                    switch (key2)
                    {
                        case '1':
                            Console.Clear();
                            Console.WriteLine("test metod");
                            Console.ReadKey();
                            break;
                        case '2':
                            Console.Clear();
                            // metod
                            break;
                        case '3':
                            Console.Clear();
                            // metod
                            break;
                        case '4':
                            Console.Clear();
                            // metod
                            break;
                        case '5':
                            Console.Clear();
                            // metod
                            break;
                        case '6':
                            Console.Clear();
                            // metod
                            break;
                        case '9':
                            break;
                        default:
                            Console.WriteLine("Felaktigt val, tryck på valfri knapp för att gå vidare...");
                            Console.ReadKey();
                            break;
                    }
                    break;
                case '2':
                    Console.Clear();
                    Console.WriteLine("Tryck på siffran för det du vill ha info om\n");
                    Menu.ShowInsideMenu();
                    
                    var key3 = Console.ReadKey(true).KeyChar;

                    switch (key3)
                    {
                        case '1':
                            Console.Clear();
                            Console.WriteLine("test metod");
                            Console.ReadKey();
                            break;
                        case '2':
                            Console.Clear();
                            // metod
                            break;
                        case '3':
                            Console.Clear();
                            // metod
                            break;
                        case '4':
                            Console.Clear();
                            // metod
                            break;
                        case '5':
                            Console.Clear();
                            // metod
                            break;
                        case '9':
                            // metod
                            break;
                        default:
                            Console.WriteLine("Felaktigt val, tryck på valfri knapp för att gå vidare...");
                            Console.ReadKey();
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Felaktigt val, tryck på valfri knapp för att gå vidare...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}