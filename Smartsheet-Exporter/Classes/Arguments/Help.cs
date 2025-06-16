namespace Smartsheet_Exporter.Classes.Arguments
{
    internal static class Help
    {
        public static bool Handler(
            string[] args,
            out string? parsedInputFilePath,
            out string? parsedFileType,
            out string? parsedOutputDir,
            out string? outputText
        )
        {
            parsedInputFilePath = null;
            parsedFileType = null;
            parsedOutputDir = null;

            outputText = "Usage: SmartsheetExporter.exe <arguments>\n";
            outputText += "Example: SmartsheetExporter.exe --token <api_token> --outputPath <some_dir_path>\n";
            outputText += "<api_token> should be a generated smartsheet api token (see https://help.smartsheet.com/articles/2482389-generate-API-key)\n";
            outputText += "\n";
            outputText += "Available arguments:\n";

            foreach (var command in ArgumentsHandler.Commands)
            {
                outputText += $"{command.Value.Key}: {command.Value.Description}\n";
                outputText += $"  Aliases: {command.Value.Aliases}\n";
                outputText += "\n";
            }

            return false;
        }
    }
}
