namespace Folder4Files.Helpers
{
    public static class DirectoryOperations
    {
        public static int MoveFileToNewDirectory(string file, string directory, DirectoryInfo newDirectory)
        {
            string newDirectoryPath = newDirectory.FullName;
            string fileNameWithExtension = Path.GetFileName(file);
            string detinationFilePath = Path.Combine(newDirectoryPath, fileNameWithExtension);

            if (FileOperations.ShouldMoveFileToNewDirection(file, directory))
            {
                try
                {
                    if (!File.Exists(detinationFilePath))
                    {
                        File.Move(file, detinationFilePath);

                        if (File.Exists(detinationFilePath))
                            ConsoleOperations.WriteSuccess($"The file {fileNameWithExtension} has been moved!\n");
                        else
                            ConsoleOperations.WriteError($"Failed to moving file: {fileNameWithExtension}");
                    }
                    else
                    {
                        ConsoleOperations.WriteWarning($"There is already a folder {newDirectoryPath} containing the file '{fileNameWithExtension}'.\n");
                        ConsoleOperations.WriteWarning($"The file '{file}' was not been moved.\n");
                        return 0;
                    }
                }
                catch (IOException ex)
                {
                    ConsoleOperations.WriteError($"Error moving file '{fileNameWithExtension}'. Error: {ex.Message}");
                }
            }
            else
            {
                ConsoleOperations.WriteError($"The file '{fileNameWithExtension}' or '{newDirectoryPath}' folder does not exist in '{Program.RootFolderPath}'.");
            }
            return 1;
        }

        public static DirectoryInfo CreateNewDirectory(string path)
        {
            if (!Directory.Exists(path))
                return Directory.CreateDirectory(path);

            return new DirectoryInfo(path);
        }
    }
}
