using System;
using System.IO;
using System.Text;

namespace HomeHunter.Services.MLDataGather
{
    class Program
    {
        private const string FilePath = @"../../../imot.bg-raw-data-2019-08-23.csv";
        private const string Separator = ",";

        static void Main(string[] args)
        {
            //23990 records in imot.bg-raw-data-2019-08-23.csv
            //16732 records in imot.bg-cleared-data-2019-08-23.csv PRICE > 0; AREA > 0 
            var properties = new ImotBgDataGatherer().GatherData(10, 1000).GetAwaiter().GetResult();
            StringBuilder sb = new StringBuilder();

            int id = 1;
            sb.AppendLine(string.Join(Separator, "Id,Url,Size,Floor,TotalFloors,CentralHeating,District,Year,Type,BuildingType,Price"));
            foreach (var row in properties)
            {
                sb.AppendLine(string.Join(Separator, $"{id},{row.Url},{row.Size},{row.Floor},{row.TotalFloors},{row.CentralHeating},\"{row.District}\",{row.Year},{row.Type},{row.BuildingType},{row.Price}"));
                id++;
            };

            File.WriteAllText(FilePath, sb.ToString());
        }
    }
}
