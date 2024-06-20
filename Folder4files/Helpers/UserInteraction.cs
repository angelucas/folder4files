namespace Folder4Files.Helpers
{
    public static class UserInteraction
    {
        private static readonly string _rootFolderPath = Program.RootFolderPath;

        public static bool ShouldProceed()
        {
            Console.Write("The current directory is: ");
            ConsoleOperations.WriteWarning(_rootFolderPath);
            Console.WriteLine("\nDo you want to organize all files in the current directory into their respective separate folders? (Y/N)");

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Y)
                {
                    Console.WriteLine("\n\n------> Starting to organize files...\n");
                    return true;
                }
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    Console.WriteLine("\nFile organization canceled!");
                    return false;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please press 'Y' or 'N'.");
                }
            } while (true);
        }
    }
}
