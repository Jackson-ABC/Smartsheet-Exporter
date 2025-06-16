namespace Smartsheet_Exporter.Classes
{
    public class ParsedArguments
    {
        public string InputFilePath { get; set; } = "";
        public string OutputPath { get; set; } = "";
        public string FileType { get; set; } = "";
    }

    public class ArgumentsHandler
    {
        public static Dictionary<string, Command> Commands = new Dictionary<string, Command>();
        static ArgumentsHandler()
        {
            Commands.Add("help",
                new Command("help",
                    "--help; -h",
                    "Display this help message",
                    Arguments.Help.Handler
                )
            );
        }
    }
}
