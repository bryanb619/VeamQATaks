namespace SyncTask
{
    /// <summary>
    /// Simple interface for file handling
    /// It's purpose is in case we want to change the way we handle files!
    /// </summary>
    public interface IFileHandler
    {
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
        void CloneFolder(string sourcePath, string destFolder,
            string logTextPath);

    }
}