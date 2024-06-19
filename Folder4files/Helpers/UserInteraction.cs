namespace Folder4Files.Helpers
{
    internal static class UserInteraction
    {
        public static bool ShouldProceed()
        {
            Console.Write("The current directory is: ");
            ConsoleOperations.WriteWarning(Program.RootFolderPath);
            Console.WriteLine("\nDo you want to organize all files in the current directory into their respective separate folders? (Y/N)");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Y)
            {
                Console.WriteLine("\n\n------> Starting to organize files...\n");
                return true;
            }
            else if (keyInfo.Key == ConsoleKey.N)
            {
                ConsoleOperations.WriteWarning("File organization canceled!");
            }

            return false;
        }
    }
}
