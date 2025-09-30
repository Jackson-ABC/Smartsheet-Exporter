using Smartsheet.Api.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Smartsheet_Exporter.Classes
{
    internal static class ExportHandler
    {
        public static void Go(Sheet sheet, string saveDir)
        {
            var export = new
            {
                id = sheet.Id,
                name = sheet.Name,
                columns = sheet.Columns.Select(
                    column => new
                    {
                        id = column.Id,
                        name = column.Title,
                        description = column.Description ?? "",
                        formula = column.Formula ?? "",
                        hidden = column.Hidden ?? false,
                        options = column.Options ?? new List<string>(),
                        primary = column.Primary ?? false,
                        symbol = column.Symbol?.ToString(),
                        type = column.Type.ToString(),
                        validation = column.Validation ?? false,
                    }
                ),
                samplerow = sheet.Rows.FirstOrDefault()?.Cells
                    .Select((cell, i) => new { i, cell.DisplayValue, cell.Formula })
                    .ToDictionary(c => c.i, c => new { c.DisplayValue, c.Formula })
            };

            string json = JsonSerializer.Serialize(export, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            string safeFilename = sheet.Name
                .Replace(" ", "_")
                .Replace("\\", "")
                .Replace("/", "");

            string filePath = Path.Combine(saveDir, $"{safeFilename}-Export.json");

            try
            {
                File.WriteAllText(filePath, json);
                Debug.Print($"File '{filePath}' created successfully.");
            }
            catch (Exception ex)
            {
                Debug.Print($"An error occurred: {ex.Message}");
            }
        }
    }
}
