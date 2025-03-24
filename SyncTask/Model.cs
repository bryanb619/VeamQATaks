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

                    // 
                    _view.Message($"{fileName} copied to {filePath}",
                        System.ConsoleColor.Green);

                    //
                    UpdateLogText($"{fileName} copied to {filePath}");

                }

                catch (IOException e)
                {
                    // Display error message
                    _view.ErrorMesage(e.Message);
                }
            }


            SubFolderClone(sourcePath, destFolder);


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
                    }

                    UpdateLogText($"{fileName} deleted from {filePath}");

                    _view.Message($"{fileName} deleted from {filePath}",
                        System.ConsoleColor.Yellow);
                }

                catch (IOException e)
                {
                    _view.ErrorMesage(e.Message);
                }
            }

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
                    }

                    UpdateLogText($"{dirName} deleted from {subDirPath}");

                    _view.Message($"{dirName} deleted from {subDirPath}",
                        System.ConsoleColor.Yellow);
                }

                catch (IOException e)
                {
                    _view.ErrorMesage(e.Message);
                }
            }

            //_view.Message(_sb.ToString());

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

                UpdateLogText($"{folderName} copied to {destSubFolderPath}");

                _view.Message($"{folderName} copied to {destSubFolderPath}",
                    System.ConsoleColor.Green);

                // Recursively call itselft until no sub folder is left
                SubFolderClone(subFolderPath, destSubFolderPath);
            }
        }


        private void UpdateLogText(string logText)
        {
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