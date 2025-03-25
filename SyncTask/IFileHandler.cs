namespace SyncTask
{
    public interface IFileHandler
    {
        void CloneFolder(string sourcePath, string destFolder,
            string logTextPath);

    }
}