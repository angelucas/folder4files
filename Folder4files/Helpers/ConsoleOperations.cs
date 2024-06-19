namespace Folder4Files.Helpers
{
    public static class ConsoleOperations
    {
        public static void WriteLine(string text, ConsoleColor? color = null)
        {
            if (color.HasValue)
            {
                var oldColor = Console.ForegroundColor;
                if (color == oldColor)
                    Console.WriteLine(text);
                else
                {
                    Console.ForegroundColor = color.Value;
                    Console.WriteLine(text);
                    Console.ForegroundColor = oldColor;
                }
            }
            else
                Console.WriteLine(text);
        }
        public static void WriteSuccess(string text)
        {
            WriteLine(text, ConsoleColor.Green);
        }
        public static void WriteError(string text)
        {
            WriteLine(text, ConsoleColor.Red);
        }
        public static void WriteWarning(string text)
        {
            WriteLine(text, ConsoleColor.DarkYellow);
        }
        public static void WriteInfo(string text)
        {
            WriteLine(text, ConsoleColor.DarkCyan);
        }
    }
}
