using System;
using System.IO;
using System.Text;


namespace SyncTask
{
    public class Model
    {
        /// <summary>
        /// Reference to the view
        /// </summary>
        private readonly IView _view;

        /// <summary>
        /// 
        /// </summary>
        private readonly StringBuilder _sb;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public Model(IView view)
        {
            _view = view;

            _sb = new StringBuilder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destFolder"></param>
        /// <param name="interval"></param>
        /// <param name="logTextPath"></param>
        public void CloneFolder(string sourcePath, string destFolder,
            int interval, string logTextPath)
        {

            // Create destination folder directory
            Directory.CreateDirectory(destFolder);

            // Foreach file in the directory
            // Obtain and copy all files!
            foreach (string filePath in Directory.GetFiles(sourcePath))
            {
                try
                {
                    // Get file name
                    string fileName = Path.GetFileName(filePath);

                    // path of destination file
                    string destFilePath = Path.Combine(destFolder, fileName);

                    // Copy file, with file destination & overwrite it
                    // Any file will be overwritten!
                    File.Copy(filePath, destFilePath, true);

                    AddLog($"{fileName} copied to {destFilePath}",
                        ConsoleColor.Green);

                }

                catch (IOException e)
                {
                    // Display error message
                    _view.ErrorMesage(e.Message);
                }
            }


            SubFolderClone(sourcePath, destFolder);

            RemoveClonedFile(destFolder, sourcePath);

            RemoveClonedDirectories(destFolder, sourcePath);


            // Remove directories not in the source
            foreach (string subDirPath in Directory.GetDirectories(destFolder))
            {

                try
                {
                    // Get directory name
                    string dirName = Path.GetFileName(subDirPath);

                    // If directory does not exist in source, delete it!
                    if (!Directory.Exists(Path.Combine(sourcePath, dirName)))
                    {
                        // delete directory with given path
                        Directory.Delete(subDirPath, true);

                        AddLog($"{dirName} deleted from {subDirPath}",
                            ConsoleColor.Red);
                    }


                }

                catch (IOException e)
                {
                    _view.ErrorMesage(e.Message);
                }

            }

            WriteLogFile(logTextPath);
        }

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

                AddLog($"{folderName} copied to {destSubFolderPath}",
                    ConsoleColor.Green);

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

                    AddLog($"{fileName} copied to {destFilePath}",
                        ConsoleColor.Green);

                }
                catch (IOException e)
                {
                    _view.ErrorMesage(
                        $"Error copying file {filePath}: {e.Message}");
                }
            }
        }

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

                        AddLog($"{fileName} deleted from {filePath}",
                            ConsoleColor.Red);
                    }
                }

                catch (IOException e)
                {
                    _view.ErrorMesage(e.Message);
                }
            }

        }


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


                        AddLog($"Directory: {dirName} deleted from: {subDirPath}",
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
                    _view.ErrorMesage($"Error deleting directory {subDirPath}: {e.Message}");
                }
            }
        }


        private void AddLog(string logText, ConsoleColor color)
        {
            _view.Message(logText, color);

            _sb.AppendLine(logText + "\n");
        }

        private void WriteLogFile(string path)
        {
            try
            {

                string directory = Path.GetDirectoryName(path);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using StreamWriter sw = new StreamWriter(path);
                sw.Write(_sb.ToString());

                _view.Message($"Log file created at {path}",
                    System.ConsoleColor.Green);
            }

            catch (IOException e)
            {
                _view.ErrorMesage(e.Message);
            }
        }
    }
}