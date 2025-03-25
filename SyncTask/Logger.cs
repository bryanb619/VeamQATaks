using System;
using System.IO;
using System.Text;

namespace SyncTask
{
    /// <summary>
    /// logger class, defines all methods to log messages & operates logic to 
    /// create log file.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Instance variable to hold the view class reference
        /// </summary>
        private readonly IView _view;

        /// <summary>
        /// Instance variable to hold log text
        /// </summary>
        private readonly StringBuilder _sb;

        /// <summary>
        /// Public constructor of Logger class.
        /// Utilizes the IView interface to set the view class 
        /// and initialize the string builder.
        /// </summary>
        /// <param name="view">reference of IView to be used to call UI</param>
        public Logger(IView view)
        {
            // set the view class
            _view = view;

            // initialize string builder
            _sb = new StringBuilder();
        }

        /// <summary>
        /// Add Message to log override number 1.
        /// Adds a message to the log and displays it in the console.
        /// Also changes the color of the text in the console.
        /// Finaly, calls UpdateLogText to add the message to the log file.
        /// </summary>
        /// <param name="logText">text to be logged</param>
        /// <param name="color">Color of text in console</param>
        public void AddLog(string logText, ConsoleColor color)
        {
            // display message in console
            _view.Message(logText, color);

            // update log text
            UpdateLogText(logText);
        }

        /// <summary>
        /// Add a message to the log override number 2.
        /// Adds a message to the log and displays it in the console.
        /// Finaly, calls UpdateLogText to add the message to the log file
        /// </summary>
        /// <param name="logText"></param>
        public void AddLog(string logText)
        {
            // display message in console
            _view.Message(logText);

            // update log text
            UpdateLogText(logText);
        }


        /// <summary>
        /// Method to update the log text
        /// Utilizes StringBuilder to append each text
        /// </summary>
        /// <param name="logText">string to be added to StringBuilder</param>
        private void UpdateLogText(string logText)
        {
            // add log to string builder
            _sb.AppendLine(logText + "\n");
        }

        /// <summary>
        /// Method to reset the log
        /// </summary>
        public void ResetLog()
        {
            // clear the string builder
            _sb.Clear();
        }

        /// <summary>
        /// Method to write log file
        /// Creates a log file and writes the log to it.
        /// If the directory does not exist, it creates it.
        /// If an exception occurs, it logs the error message to the console.
        /// If the log file is created successfully, it displays the path of the log file.
        /// </summary>
        /// <param name="path">Path of log file</param>
        public void WriteLogFile(string path)
        {
            try
            {
                // append final message to log
                _sb.AppendLine("\nThanks for using SyncTask!\n");

                // get directory path from file path
                string directory = Path.GetDirectoryName(path);

                // check if directory exists
                if (!Directory.Exists(directory))
                {
                    // create directory if it doesn't exist
                    Directory.CreateDirectory(directory);
                }

                // Create a new file and write the log to it
                using StreamWriter sw = new StreamWriter(path);

                // write log to file
                sw.Write(_sb.ToString());

                // display path of log message in console
                _view.Message($"Log file created at {path}",
                    ConsoleColor.DarkBlue);
            }

            // catch exception & log it to console
            catch (IOException e)
            {
                // display error message
                _view.ErrorMesage(e.Message);
            }
        }
    }
}