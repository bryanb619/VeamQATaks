using System;
using System.IO;
using System.Text;

namespace SyncTask
{
    public class Logger
    {
        /// <summary>
        /// 
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
        public Logger(IView view)
        {
            // set the view class
            _view = view;

            // initialize string builder
            _sb = new StringBuilder();
        }


        public void AddLog(string logText, ConsoleColor color)
        {
            // display message in console
            _view.Message(logText, color);

            UpdateLogText(logText);
        }

        public void AddLog(string logText)
        {
            // display message in console
            _view.Message(logText);

            UpdateLogText(logText);
        }

        private void UpdateLogText(string logText)
        {
            // add log to string builder
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

                // Create a new file and write the log to it
                using StreamWriter sw = new StreamWriter(path);

                sw.Write(_sb.ToString());

                // display path of log message in console
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