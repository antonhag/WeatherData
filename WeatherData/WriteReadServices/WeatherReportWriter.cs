namespace WeatherData.WeatherServices;

public class WeatherReportWriter
{
    private static string path = "../../../Files/";
    
    public static async Task WriteReport(string fileName)
    {
        List<string> avgTempData = WeatherAnalysis.GetAverageByMonth(r => r.Temp, "temperatur");
        
        List<string> avgHumidity = WeatherAnalysis.GetAverageByMonth(r => r.Humidity, "fuktighet");
        
        List<string> avgMoldRisk = WeatherAnalysis.GetAverageMoldRiskByMonth();
        
        string autumn = WeatherAnalysis.GetMeteorologicalSeason(10.0);
        string winter = WeatherAnalysis.GetMeteorologicalSeason(0.0);

        using (StreamWriter writer = new StreamWriter(path + fileName))
        {
            foreach (var line in avgTempData)
            {
                await writer.WriteLineAsync(line);
            }
            
            foreach (var line in avgHumidity)
            {
                await writer.WriteLineAsync(line);
            }

            foreach (var line in avgMoldRisk)
            {
                await writer.WriteLineAsync(line);
            }

            await writer.WriteLineAsync(autumn);
            await writer.WriteLineAsync(winter);
        }
    }
    
}