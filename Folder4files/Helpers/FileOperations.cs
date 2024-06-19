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
                string rawFileName = RemoveDiscInfo(Path.GetFileNameWithoutExtension(file));

                if (ShouldMoveFile(file))
                {
                    ConsoleOperations.WriteInfo($"Processing {rawFileName} file transfer...");
                    DirectoryInfo newDirectory = DirectoryOperations.CreateNewDirectory(rawFileName);
                    filesMoved += DirectoryOperations.MoveFileToNewDirectory(file, rawFileName, newDirectory);
                }
                else if (!IsIgnoredFile(file))
                {
                    ConsoleOperations.WriteWarning($"That path already exists: {Path.Combine(Program.RootFolderPath, rawFileName)}");
                }
            }

            ConsoleOperations.WriteWarning($"{filesMoved} files were moved!");
        }
        public static bool ShouldMoveFile(string file)
        {
            return File.Exists(file) && !IsIgnoredFile(file);
        }
        public static bool ShouldMoveFileToNewDirection(string file, string directory)
        {
            return File.Exists(file) && Directory.Exists(directory);
        }
        public static string RemoveDiscInfo(string fileName)
        {
            return Regex.Replace(fileName, DiscInfoPattern, "");
        }
        public static bool IsIgnoredFile(string path)
        {
            string fileName = Path.GetFileName(path);
            return fileName.Equals(Program.IgnoreFile, StringComparison.OrdinalIgnoreCase);
        }
    }
}
