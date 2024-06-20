using Folder4Files.Helpers;

namespace Folder4Files.Tests.Helpers
{
    public class FileOperationsTests
    {
        [Fact]
        public void RemoveDiscInfo_RemovesDiscInfoFromFileName()
        {
            // Arrange
            string fileNameWithDiscInfo = "Alone in the Dark - The New Nightmare (Disc 1)";
            string expected = "Alone in the Dark - The New Nightmare";

            // Act
            string result = FileOperations.RemoveDiscInfo(fileNameWithDiscInfo);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Folder4Files.exe", true)]
        [InlineData("Parasite Eve II (Disc 1).chd", false)]
        public void IsIgnoredFile_ReturnsCorrectlyBasedOnFileName(string fileName, bool expected)
        {
            // Arrange
            string filePath = Path.Combine(Program.RootFolderPath, fileName);

            // Act
            bool result = FileOperations.IsIgnoredFile(filePath);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldMoveFile_ReturnsTrue_WhenFileExistsAndIsNotIgnored()
        {
            // Arrange
            string filePath = "Ehrgeiz - God Bless the Ring.chd";
            File.Create(filePath).Dispose();

            try
            {
                // Act
                bool result = FileOperations.ShouldMoveFile(filePath);

                // Assert
                Assert.True(result);
            }
            finally
            {
                File.Delete(filePath); // Clean up
            }
        }

        [Fact]
        public void ShouldMoveFile_ReturnsFalse_WhenFileDoesNotExist()
        {
            // Arrange
            string filePath = "Fatal Fury";

            // Act
            bool result = FileOperations.ShouldMoveFile(filePath);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ShouldMoveFileToNewDirection_ReturnsTrue_WhenFileAndDirectoryExist()
        {
            // Arrange
            string filePath = "Gran Turismo (USA)";
            string directoryPath = "testDirectory";
            Directory.CreateDirectory(directoryPath);
            File.Create(filePath).Dispose();

            try
            {
                // Act
                bool result = FileOperations.ShouldMoveFileToNewDirection(filePath, directoryPath);

                // Assert
                Assert.True(result);
            }
            finally
            {
                // Clean up
                File.Delete(filePath);
                Directory.Delete(directoryPath);
            }
        }

        [Fact]
        public void ShouldMoveFileToNewDirection_ReturnsFalse_WhenFileDoesNotExist()
        {
            // Arrange
            string filePath = "nonexistentfile.txt";
            string directoryPath = "testDirectory";
            Directory.CreateDirectory(directoryPath);

            try
            {
                // Act
                bool result = FileOperations.ShouldMoveFileToNewDirection(filePath, directoryPath);

                // Assert
                Assert.False(result);
            }
            finally
            {
                // Clean up
                Directory.Delete(directoryPath);
            }
        }

        [Fact]
        public void ShouldMoveFileToNewDirection_ReturnsFalse_WhenDirectoryDoesNotExist()
        {
            // Arrange
            string filePath = "test.txt";
            string directoryPath = "nonexistentDirectory";
            File.Create(filePath).Dispose();

            try
            {
                // Act
                bool result = FileOperations.ShouldMoveFileToNewDirection(filePath, directoryPath);

                // Assert
                Assert.False(result);
            }
            finally
            {
                // Clean up
                File.Delete(filePath);
            }
        }
    }
}
