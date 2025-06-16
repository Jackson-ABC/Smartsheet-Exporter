using Smartsheet_Exporter.Classes;

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

            if (!ArgumentsHandler.HandleArguments(args, out string? inputFilePath, out string? fileType, out string? outputDir, out string? outputText))
            {
                Console.WriteLine(outputText);
                return;
            }
        }
    }
}