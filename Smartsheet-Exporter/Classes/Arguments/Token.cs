using Smartsheet.Api;
using Smartsheet.Api.Models;

namespace Smartsheet_Exporter.Classes.Arguments
{
    internal class Token
    {
        public static string Name => "token";
        public static string[] Flags => new string[] { "--token", "-t" };
        public static string FlagsString => "--token; -t";
        public static string Description => "Set the Smartsheet Access Token to use";

        public static string Help => $"Run `{Settings.Default.ProgramName} {Flags[0]} <SmartsheetAccessToken>` to set.";

        public static string Value => Settings.Default.SmartsheetAccessToken;

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
            outputText = "";

            return false;
        }

        public static bool Validate(out string errorString)
        {

            if (Value == "!NotSet!")
            {
                errorString = "Error: Smartsheet Access Token not set.\n";
                errorString += Help;
                
                return false;
            }

            try
            {
                SmartsheetClient smartsheetClient = new SmartsheetBuilder()
                    .SetAccessToken(Value)
                    .Build();

                UserProfile user = smartsheetClient.UserResources.GetCurrentUser();

                errorString = "";
                return true;
            }
            catch (AuthorizationException)
            {
                errorString = "Error: Smarsheet Access Token not authorised.\n";
                errorString += "Smarsheet Access Token is invalid or expired.\n";
                errorString += Help;

                return false;
            }
            catch (Exception ex)
            {
                errorString = "Cmd: Token.Validate\n";
                errorString += $"Unexpected error: {ex.Message}";
                
                return false;
            }
        }
    }
}
