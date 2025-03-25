using System;
using System.IO;
using System.Text;

namespace SyncTask
{
    public class Logger
    {
        private readonly IView _view;

        private readonly StringBuilder _sb;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public Logger(IView view)
        {
            _view = view;

            _sb = new StringBuilder();
        }


        public void AddLog(string logText, ConsoleColor color)
        {
            _view.Message(logText, color);

            _sb.AppendLine(logText + "\n");
        }

        public void WriteLogFile(string path)
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
                    ConsoleColor.Green);
            }

            catch (IOException e)
            {
                _view.ErrorMesage(e.Message);
            }
        }
    }
}