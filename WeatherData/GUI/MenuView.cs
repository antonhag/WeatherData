namespace WeatherData;

public class MenuView
{
    private enum MenuOption
    {
        Utomhus = 1,
        Inomhus = 2
    }
    
    private enum OutsideInfo
    {
        Visa_medeltemperatur_och_luftfuktighet_per_dag_för_valt_datum = 1,
        Sortera_varmast_till_kallaste_dagen_enligt_medeltemperatur_per_dag,
        Sortera_torrast_till_fuktigast_dagen_enligt_medelfuktighet_per_dag,
        Sortera_minst_till_störst_risk_av_mögel,
        Visa_datum_för_meteorologisk_höst,
        Visa_datum_för_meteorologisk_vinter,
        Återgå_till_menyn = 9
    }

    private enum InsideInfo
    {
        Visa_medeltemperatur_för_valt_datum = 1,
        Sortera_varmast_till_kallaste_dagen_enligt_medeltemperatur_per_dag,
        Sortera_torrast_till_fuktigast_dagen_enligt_medelfuktighet_per_dag,
        Sortera_minst_till_störst_risk_av_mögel,
        Återgå_till_menyn = 9
    }

    public static void ShowMenu()
    {
        var options = new List<string>();
        
        foreach (int i in Enum.GetValues(typeof(MenuOption)))
        {
            options.Add($"{i}. {Enum.GetName(typeof(MenuOption), i).Replace("_", " ")}");
        }


        Console.WriteLine("Välj vad du vill få info om\n");
        foreach (var option in options)
        {
            Console.WriteLine(option);
        }
    }

    public static void ShowOutsideMenu()
    {
        var options = new List<string>();
        
        foreach (int i in Enum.GetValues(typeof(OutsideInfo)))
        {
            options.Add($"{i}. {Enum.GetName(typeof(OutsideInfo), i).Replace("_", " ")}");
        }
        
        foreach (var option in options)
        {
            Console.WriteLine(option);
        }
    }

    public static void ShowInsideMenu()
    {
        var options = new List<string>();
        
        foreach (int i in Enum.GetValues(typeof(InsideInfo)))
        {
            options.Add($"{i}. {Enum.GetName(typeof(InsideInfo), i).Replace("_", " ")}");
        }

        foreach (var option in options)
        {
            Console.WriteLine(option);
        }
    }

}