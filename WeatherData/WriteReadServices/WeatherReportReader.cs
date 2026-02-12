namespace WeatherData.WeatherServices;

public class WeatherReportReader
{
    private static string path = "../../../Files/";

    public static void ReadReport(string fileName)
    {
        Console.Clear();
        try
        {
            using (StreamReader reader = new StreamReader(path + fileName))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    Console.WriteLine(line);
                    line = reader.ReadLine();
                }
            }
            
            Console.WriteLine("\nTryck valfri knapp för att gå tillbaka...");
            Console.ReadKey();
        }
        catch
        {
            Console.WriteLine("Filen finns inte");
        }
    }
}