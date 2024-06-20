using Folder4Files.Helpers;

namespace Folder4Files
{
    public class Program
    {
        public const string IgnoreFile = "Folder4files.exe";
        public static readonly string RootFolderPath = Directory.GetCurrentDirectory();
        public static readonly string[] Files = Directory.GetFiles(RootFolderPath);

        static void Main(string[] args)
        {
            try
            {
                if (UserInteraction.ShouldProceed())
                    FileOperations.ProcessFiles();
            }
            catch (IOException ex)
            {
                ConsoleOperations.WriteError($"Error processing file: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
