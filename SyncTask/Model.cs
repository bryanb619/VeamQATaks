using System.IO;

namespace SyncTask
{
    public class Model
    {

        private readonly IView _view;

        public Model(IView view)
        {
            _view = view;
        }

        /// <summary>
        /// Method copies source folder and 
        /// </summary>
        /// <param name="path"></param>
        public void replicateFolder(string sourcePath, string destFolder)
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
                }

                catch (IOException e)
                {
                    // Display error message
                    _view.ErrorMesage(e.Message);

                }
            }


            // Copy all subdirectories recursively
            foreach (string subFolderPath in Directory.GetDirectories(sourcePath))
            {
                // path of sub folder
                string folderName = Path.GetFileName(subFolderPath);

                string destSubFolderPath = Path.Combine(destFolder, folderName);

                // Recursively call itselft until no sub folder is left
                replicateFolder(subFolderPath, destSubFolderPath);
            }


            // Remove files and directories not in the source
            foreach (string filePath in Directory.GetFiles(destFolder))
            {
                // Get file name
                string fileName = Path.GetFileName(filePath);

                // If file does not exist in source, delete it!
                if (!File.Exists(Path.Combine(sourcePath, fileName)))
                {
                    // delete file witg given path
                    File.Delete(filePath);
                }
            }

            // Remove directories not in the source
            foreach (string subDirPath in Directory.GetDirectories(destFolder))
            {
                // Get directory name
                string dirName = Path.GetFileName(subDirPath);

                // If directory does not exist in source, delete it!
                if (!Directory.Exists(Path.Combine(sourcePath, dirName)))
                {
                    // delete directory with given path
                    Directory.Delete(subDirPath, true);
                }
            }
        }
    }
}