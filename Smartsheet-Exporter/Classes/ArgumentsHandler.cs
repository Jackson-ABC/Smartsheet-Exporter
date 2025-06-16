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
            Commands.Add("version",
                new Command("version",
                    "--version; -v",
                    "Display the version of the program",
                    Arguments.Version.Handler
                )
            );
        }

        public static bool HandleArguments(
            string[] args,
            out string? inputFilePath,
            out string? fileType,
            out string? outputDir,
            out string? outputText
        )
        {
            inputFilePath = null;
            fileType = null;
            outputDir = null;
            outputText = null;
            bool success = true;

            if (args.Contains("--help") || args.Contains("-h"))
            {
                Commands["help"].Handler(args,
                    out inputFilePath,
                    out fileType,
                    out outputDir,
                    out outputText
                );
                return success;
            }

            return success;
        }
    }
}
