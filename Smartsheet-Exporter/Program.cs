using Smartsheet.Api;
using Smartsheet.Api.Models;
using Smartsheet_Exporter.Classes;
using System.Security.Cryptography;

namespace Smartsheet_Exporter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Smartsheet Exporter - Command Line Interface");
            Console.WriteLine("============================================");
            Console.WriteLine("A tool for exporting data from smartsheet to enable tracking in git");
            Console.WriteLine();

            Console.WriteLine("Input Smartsheet API Key:");
            string apiKey = Console.ReadLine() ?? "";

            Console.WriteLine("Input Smartsheet sheet id:");
            string sheetID_str = Console.ReadLine() ?? "";

            Console.WriteLine("Input save location:");
            string savePath = Console.ReadLine() ?? "";

            long sheetID = long.Parse(sheetID_str);

            SmartsheetClient client = new SmartsheetBuilder()
                .SetAccessToken(apiKey)
                .Build();

            Sheet sheet = client.SheetResources.GetSheet(sheetID);
            
            ExportHandler.Go(sheet, savePath);

            /*if (!ArgumentsHandler.HandleArguments(args, out string? inputFilePath, out string? fileType, out string? outputDir, out string? outputText))
            {
                Console.WriteLine(outputText);
                return;
            }*/
        }
    }
}