using System;
using System.IO;
using System.Text.RegularExpressions;

string rootFolderPath = Directory.GetCurrentDirectory();
string[] filesDirs = Directory.GetFiles(rootFolderPath);
string fileName = string.Empty;
string ignoreFile = "folder4files.exe";
string ignoreFile2 = "folder4files.pdb";

try
{
    Console.WriteLine("The current directory is {0}\n", rootFolderPath);

    foreach (string fileDir in filesDirs)
    {
        if (File.Exists(fileDir) && !CheckFileFolderExistence(fileDir) && !CheckIgnoredFiles(fileDir))
        {
            Console.WriteLine($"Moving: {fileDir}\n");
            RemoveDiscInfo();
            DirectoryInfo destDirectory = CreateNewDirectory(fileName);
            MoveFileToNewDirectory(fileDir, destDirectory);
        }
        else
        {
            Console.WriteLine("That path already exists: " + fileName);
        }
    }

    Console.ReadKey();
}
catch (Exception e)
{
    Console.WriteLine("The process failed: {0}", e.ToString());
}
finally {}

bool CheckIgnoredFiles(string fileDir)
{
    string file = Path.GetFileName(fileDir);
    if (ignoreFile == file || ignoreFile2 == file)
        return true;

    return false;
}

bool CheckFileFolderExistence(string fileDir)
{
    fileName = Path.GetFileNameWithoutExtension(fileDir);
    if (Directory.Exists(fileName))
        return true;

    return false;
}

void MoveFileToNewDirectory(string fileNameDir, DirectoryInfo destFileDir)
{
    string fileNameAndExtention = Path.GetFileName(fileNameDir);
    string finalPath = Path.Combine(destFileDir.FullName, fileNameAndExtention);
    File.Move(fileNameDir, finalPath);
    Console.WriteLine($"Moved: {fileNameAndExtention}");
}

void RemoveDiscInfo()
{
    fileName = Regex.Replace(fileName, @" \(Disc \d+\)", "");
}

DirectoryInfo CreateNewDirectory(string fileName)
{
    return Directory.CreateDirectory(fileName);
}