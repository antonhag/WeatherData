using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData.Files
{
    public class ReadWriteFiles
    {
        public static string path = "../../../Files/";

        public static void ReadAll(string fileName)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path + fileName))
                {
                    string line = reader.ReadLine();
                    int rowCount = 0;
                    while (line != null)
                    {
                        Console.WriteLine(rowCount + " " + line);
                        rowCount++;
                        line = reader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Filen finns inte ");
            }
        }
        public static async Task WriteRow(string fileName, string text)
        {
            using (StreamWriter streamwriter = new StreamWriter(path + fileName, true))
            {
                await streamwriter.WriteLineAsync(text);
            }
        }
    }
}

