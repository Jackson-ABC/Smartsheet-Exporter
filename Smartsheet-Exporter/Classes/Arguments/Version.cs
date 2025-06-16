using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartsheet_Exporter.Classes.Arguments
{
    internal class Version
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

            outputText = $"Version {Settings.Default.CurrentVersion}\n";
            return false;
        }
    }
}