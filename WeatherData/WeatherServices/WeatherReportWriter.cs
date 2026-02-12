namespace WeatherData.WeatherServices;

public class WeatherReportWriter
{
    private static string path = "../../../Files/";
    
    public static async Task WriteAvgTempByMonth(string fileName)
    {
        List<string> avgTempData = WeatherAnalysis.GetAvgTempByMonth();

        using (StreamWriter writer = new StreamWriter(path + fileName))
        {
            foreach (var line in avgTempData)
            {
                await writer.WriteLineAsync(line);
            }
        }
    }
}