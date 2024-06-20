using Moq;
using Folder4Files.Helpers;

namespace Folder4Files.Tests.Helpers
{
    public class DirectoryOperationsTests
    {
        [Fact]
        public void CreateNewDirectory_DirectoryDoesNotExist_CreatesDirectory()
        {
            // Arrange
            string path = Path.Combine(Directory.GetCurrentDirectory(), "TestDirectory");
            if (Directory.Exists(path))
                Directory.Delete(path, true);

            // Act
            DirectoryInfo result = DirectoryOperations.CreateNewDirectory(path);

            // Assert
            Assert.True(Directory.Exists(path));
            Assert.Equal(path, result.FullName);

            // Cleanup
            Directory.Delete(path, true);
        }

        [Fact]
        public void CreateNewDirectory_DirectoryExists_ReturnsDirectoryInfo()
        {
            // Arrange
            string path = Path.Combine(Directory.GetCurrentDirectory(), "TestDirectory");
            Directory.CreateDirectory(path);

            // Act
            DirectoryInfo result = DirectoryOperations.CreateNewDirectory(path);

            // Assert
            Assert.True(Directory.Exists(path));
            Assert.Equal(path, result.FullName);

            // Cleanup
            Directory.Delete(path, true);
        }

        [Fact]
        public void MoveFileToNewDirectory_FileAndDirectoryExist_FileMovedSuccessfully()
        {
            // Arrange
            string sourceDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SourceDirectory");
            string destinationDirectory = Path.Combine(Directory.GetCurrentDirectory(), "DestinationDirectory");
            string fileName = "testfile.txt";
            string sourceFilePath = Path.Combine(sourceDirectory, fileName);
            string destinationFilePath = Path.Combine(destinationDirectory, fileName);

            Directory.CreateDirectory(sourceDirectory);
            Directory.CreateDirectory(destinationDirectory);
            File.WriteAllText(sourceFilePath, "Test content");

            var newDirectoryInfo = new DirectoryInfo(destinationDirectory);

            // Mock ConsoleOperations
            var consoleOperationsMock = new Mock<ConsoleOperations>();

            // Act
            int result = DirectoryOperations.MoveFileToNewDirectory(sourceFilePath, destinationDirectory, newDirectoryInfo);

            // Assert
            Assert.Equal(1, result);
            Assert.True(File.Exists(destinationFilePath));
            Assert.False(File.Exists(sourceFilePath));

            // Cleanup
            if (Directory.Exists(sourceDirectory)) Directory.Delete(sourceDirectory, true);
            if (Directory.Exists(destinationDirectory)) Directory.Delete(destinationDirectory, true);
        }

        [Fact]
        public void MoveFileToNewDirectory_FileExistsInDestination_FileNotMoved()
        {
            // Arrange
            string sourceDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SourceDirectory");
            string destinationDirectory = Path.Combine(Directory.GetCurrentDirectory(), "DestinationDirectory");
            string fileName = "testfile.txt";
            string sourceFilePath = Path.Combine(sourceDirectory, fileName);
            string destinationFilePath = Path.Combine(destinationDirectory, fileName);

            Directory.CreateDirectory(sourceDirectory);
            Directory.CreateDirectory(destinationDirectory);
            File.WriteAllText(sourceFilePath, "Test content");
            File.WriteAllText(destinationFilePath, "Test content");

            var newDirectoryInfo = new DirectoryInfo(destinationDirectory);

            // Mock ConsoleOperations
            var consoleOperationsMock = new Mock<ConsoleOperations>();

            // Act
            int result = DirectoryOperations.MoveFileToNewDirectory(sourceFilePath, destinationDirectory, newDirectoryInfo);

            // Assert
            Assert.Equal(0, result);
            Assert.True(File.Exists(sourceFilePath));
            Assert.True(File.Exists(destinationFilePath));

            // Cleanup
            if (Directory.Exists(sourceDirectory)) Directory.Delete(sourceDirectory, true);
            if (Directory.Exists(destinationDirectory)) Directory.Delete(destinationDirectory, true);
        }
    }
}
