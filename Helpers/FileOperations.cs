using System.Text.RegularExpressions;

namespace Folder4Files.Helpers
{
    internal static class FileOperations
    {
        private const string DiscInfoPattern = @" \(Disc \d+\)";

        public static void ProcessFiles()
        {
            int filesMoved = 0;

            foreach (string file in Program.Files)
            {
                string cleanFileName = RemoveDiscInfo(Path.GetFileNameWithoutExtension(file));

                if (ShouldMoveFile(file, cleanFileName))
                {
                    ConsoleOperations.WriteInfo($"Processing {cleanFileName} file transfer...");
                    DirectoryInfo destinationFileDirectory = DirectoryOperations.CreateNewDirectory(cleanFileName);
                    filesMoved += DirectoryOperations.MoveFileToNewDirectory(file, cleanFileName, destinationFileDirectory);
                }
                else if (!IsIgnoredFile(file))
                {
                    ConsoleOperations.WriteWarning($"That path already exists: {Path.Combine(Program.RootFolderPath, cleanFileName)}");
                }
            }

            ConsoleOperations.WriteWarning($"{filesMoved} files were moved!");
        }
        public static bool ShouldMoveFile(string file, string cleanFileName)
        {
            return File.Exists(file) && !IsIgnoredFile(file);
        }
        public static bool ShouldMoveFileToNewDirection(string file, string newDirectoryName)
        {
            return File.Exists(file) && DirectoryOperations.CheckFileFolderExistence(newDirectoryName);
        }
        public static string RemoveDiscInfo(string fileName)
        {
            return Regex.Replace(fileName, DiscInfoPattern, "");
        }
        public static bool IsIgnoredFile(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            return fileName.Equals(Program.IgnoreFile, StringComparison.OrdinalIgnoreCase);
        }
    }
}