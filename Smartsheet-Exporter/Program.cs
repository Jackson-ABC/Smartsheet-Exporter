namespace Smartsheet_Exporter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                // Display help
                return;
            }

            if (args.Contains("-h") || args.Contains("--help"))
            {
                foreach (string arg in args)
                {
                    // if arg exists
                    // Return arg help
                    // else
                    // Return $"Error. {arg} is not a valid argument"
                }
            }
        }
    }
}
