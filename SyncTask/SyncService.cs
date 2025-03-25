using System;
using System.IO;

namespace SyncTask
{

    /// <summary>
    /// Main class of program logic & data flow.
    /// SyncService class is responsible for the synchronization of files and 
    /// directories. 
    /// </summary>
    public class SyncService : IFileHandler
    {
        /// <summary>
        /// Reference to the view.
        /// Use ref to display messages to the user. 
        /// </summary>
        private readonly IView _view;

        /// <summary>
        /// Reference to logger. 
        /// Use class to add info to the log and write log file
        /// </summary>
        private readonly Logger _logger;


        /// <summary>
        /// Public constructor of sync service.
        /// Initializes the view and logger
        /// </summary>
        /// <param name="view">reference of view to be received</param>
        public SyncService(IView view)
        {
            // initialize view with given reference
            _view = view;

            // initialize logger with given view reference
            _logger = new Logger(_view);
        }

        /// <summary>
        /// Starting point SyncService class.
        /// Method to clone a folder from source to destination.
        /// In case of necessary, it will create the destination folder.
        /// It will also remove files and directories not in the source.
        /// It will log all operations to a log file.
        /// </summary>
        /// <param name="sourcePath">Source folder path</param>
        /// <param name="destFolder">Destination folder path</param>
        /// <param name="logTextPath">Log folder path</param>
        public void CloneFolder(string sourcePath, string destFolder,
            string logTextPath)
        {
            // Reset log file
            _logger.ResetLog();

            // check if source directory exists
            if (!Directory.Exists(sourcePath))
            {
                // Display error message
                LogInfo($"Source directory {sourcePath} does not exist",
                    ConsoleColor.Red);

                // Write log file
                _logger.WriteLogFile(logTextPath);

                // return
                return;
            }

            // Create destination folder directory
            Directory.CreateDirectory(destFolder);

            // Foreach file in the directory
            // Obtain and copy all files!
            foreach (string filePath in Directory.GetFiles(sourcePath))
            {
                // Try to copy file to destination
                try
                {
                    // Get file name
                    string fileName = Path.GetFileName(filePath);

                    // path of destination file
                    string destFilePath = Path.Combine(destFolder, fileName);

                    // Copy file, with file destination & overwrite it
                    // Any file will be overwritten!
                    File.Copy(filePath, destFilePath, true);

                    // Display message to user
                    LogInfo($"File: {fileName} copied to {destFilePath}");

                }

                // catch exception if file cannot be copied
                catch (IOException e)
                {
                    // Display error message
                    LogInfo($"Error copying file: {e.Message}",
                        ConsoleColor.Red);
                }

            }

            // Copy all subdirectories recursively
            SubFolderClone(sourcePath, destFolder);

            // Remove files not in the source
            RemoveClonedFile(destFolder, sourcePath);

            // Remove directories not in the source
            RemoveClonedDirectories(destFolder, sourcePath);

            // finalize all operations & write log file to given path
            _logger.WriteLogFile(logTextPath);
        }

        /// <summary>
        /// Method to clone subfolders recursively
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destFolder"></param>
        private void SubFolderClone(string sourcePath, string destFolder)
        {
            // Copy all subdirectories recursively
            foreach (string subFolderPath in Directory.GetDirectories(sourcePath))
            {
                // path of sub folder
                string folderName = Path.GetFileName(subFolderPath);

                string destSubFolderPath = Path.Combine(destFolder, folderName);

                // Create the destination subfolder
                Directory.CreateDirectory(destSubFolderPath);

                LogInfo($"Directory: {folderName} copied to {destSubFolderPath}",
                    ConsoleColor.Yellow);

                // Recursively call itselft until no sub folder is left
                SubFolderClone(subFolderPath, destSubFolderPath);
            }

            // Copy all files in the current directory
            foreach (string filePath in Directory.GetFiles(sourcePath))
            {
                try
                {
                    // Get the file name
                    string fileName = Path.GetFileName(filePath);

                    // Path of the destination file
                    string destFilePath = Path.Combine(destFolder, fileName);

                    // Copy the file, overwriting if it exists
                    File.Copy(filePath, destFilePath, true);

                    LogInfo($"File: {fileName} copied to {destFilePath}",
                        ConsoleColor.Green);

                }
                catch (IOException e)
                {
                    LogInfo($"Error copying file: {e.Message}",
                        ConsoleColor.Red);
                }
            }
        }

        /// <summary>
        /// Method to remove cloned files not in the source
        /// </summary>
        /// <param name="destFolder"></param>
        /// <param name="sourcePath"></param>
        private void RemoveClonedFile(string destFolder, string sourcePath)
        {
            // Remove files and directories not in the source
            foreach (string filePath in Directory.GetFiles(destFolder))
            {

                try
                {
                    // Get file name
                    string fileName = Path.GetFileName(filePath);

                    // If file does not exist in source, delete it!
                    if (!File.Exists(Path.Combine(sourcePath, fileName)))
                    {
                        // delete file with given path
                        File.Delete(filePath);

                        LogInfo($"{fileName} deleted from {filePath}",
                            ConsoleColor.Red);
                    }
                }

                catch (IOException e)
                {
                    LogInfo(
                        $"Error deleting file {filePath}: {e.Message}",
                        ConsoleColor.Red);
                }
            }

            // Recursively check subdirectories
            foreach (string subDirPath in Directory.GetDirectories(destFolder))
            {
                string subDir = Path.GetFileName(subDirPath);

                string correspondingSourceSubDir =
                    Path.Combine(sourcePath, subDir);

                // If the subdirectory exists in the source, process its files
                if (Directory.Exists(correspondingSourceSubDir))
                {
                    // call itself recursively
                    RemoveClonedFile(subDirPath, correspondingSourceSubDir);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destFolder"></param>
        /// <param name="sourcePath"></param>
        private void RemoveClonedDirectories(string destFolder, string sourcePath)
        {
            foreach (string subDirPath in Directory.GetDirectories(destFolder))
            {
                try
                {
                    // Get directory name
                    string dirName = Path.GetFileName(subDirPath);

                    // Path of the corresponding directory in the source
                    string sourceSubDirPath = Path.Combine(sourcePath, dirName);

                    // If the directory does not exist in the source, delete it
                    if (!Directory.Exists(sourceSubDirPath))
                    {
                        // Delete the directory and its contents
                        Directory.Delete(subDirPath, true);

                        // log info
                        LogInfo(
                            $"Directory: {dirName} deleted from: {subDirPath}",
                            ConsoleColor.Red);

                    }
                    else
                    {
                        // Recursively check subdirectories
                        RemoveClonedDirectories(subDirPath, sourceSubDirPath);
                    }
                }
                catch (IOException e)
                {

                    // log error message
                    LogInfo(
                        $"Error deleting directory {subDirPath}: {e.Message}",
                        ConsoleColor.Red);
                }
            }
        }

        /// <summary>
        /// Override Method Nº1 used to log info to the console with given text color
        /// </summary>
        /// <param name="logText">Text to be logged</param>
        /// <param name="color">color to be set in console</param>
        private void LogInfo(string logText, ConsoleColor color)
        {
            // call logger method to add log with given text and color
            _logger.AddLog(logText, color);
        }


        /// <summary>
        /// Override Method Nº2 used to log info to the console with given text
        /// </summary>
        /// <param name="logText">Text to be logged</param>
        private void LogInfo(string logText)
        {
            // call logger method to add log with given text
            _logger.AddLog(logText);
        }

    }

}