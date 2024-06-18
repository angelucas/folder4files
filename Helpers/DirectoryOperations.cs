namespace Folder4Files.Helpers
{
    internal static class DirectoryOperations
    {
        public static int MoveFileToNewDirectory(string file, string originalFileName, DirectoryInfo destinationFileDirectory)
        {
            string fileNameAndExtension = Path.GetFileName(file);
            string destFileName = Path.Combine(destinationFileDirectory.FullName, fileNameAndExtension);

            if (FileOperations.ShouldMoveFileToNewDirection(file, originalFileName))
            {
                try
                {
                    if (!File.Exists(destFileName))
                    {
                        File.Move(file, destFileName);

                        if (File.Exists(destFileName))
                            ConsoleOperations.WriteSuccess($"The file {fileNameAndExtension} has been moved!\n");
                        else
                            ConsoleOperations.WriteError($"Failed to moving file: {fileNameAndExtension}");
                    }
                    else
                    {
                        ConsoleOperations.WriteWarning($"There is already a folder {destinationFileDirectory.FullName} containing the file '{fileNameAndExtension}'.\n");
                        ConsoleOperations.WriteWarning($"The file '{file}' was not been moved.\n");
                        return 0;
                    }
                }
                catch (IOException ex)
                {
                    ConsoleOperations.WriteError($"Error moving file '{fileNameAndExtension}': {ex.Message}");
                }
            }
            else
            {
                ConsoleOperations.WriteError($"File '{fileNameAndExtension}' does not exist in the source location.");
            }
            return 1;
        }
        public static bool CheckFileFolderExistence(string file)
        {
            return Directory.Exists(file);
        }
        public static DirectoryInfo CreateNewDirectory(string directoryName)
        {
            if (!CheckFileFolderExistence(directoryName))
                return Directory.CreateDirectory(directoryName);

            return new DirectoryInfo(directoryName);
        }
    }
}
