using System;
using System.IO;

namespace SyncTask
{
    public class Model
    {
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
                //
                string fileName = Path.GetFileName(filePath);

                //
                string destFilePath = Path.Combine(destFolder, fileName);

                // Copy file, with file destination & overwrite it
                // Any file will be overwritten!
                File.Copy(filePath, destFilePath, true);
            }

            // Copy all subdirectories recursively
            foreach (string subFolderPath in Directory.GetDirectories(sourcePath))
            {
                string folderName = Path.GetFileName(subFolderPath);

                string destSubFolderPath = Path.Combine(destFolder, folderName);

                // Recursively call itselft until no sub folder is left
                replicateFolder(subFolderPath, destSubFolderPath);
            }

        }
    }
}